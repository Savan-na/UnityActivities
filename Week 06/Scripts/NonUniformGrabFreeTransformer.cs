/*
 * DISCLAIMER — AI-ASSISTED MODIFICATION
 *
 * This file ("NonUniformGrabFreeTransformer.cs") was created with assistance from ChatGPT 5 (OpenAI).
 * Process summary:
 *  1) The original Meta Interaction SDK file "GrabFreeTransformer.cs" was provided as the base reference.
 *  2) The AI was asked to add a toggle to enable non-uniform (per-axis) scaling while preserving the base
 *     position/rotation behavior.
 *  3) Additional requested options were implemented:
 *       • Lock Rotation While Scaling (freeze two-hand rotation).
 *       • Limit Scaling To Two Most-Parallel Axes (plane-only scaling in the current object space).
 *  4) Non-uniform scaling is computed in the object’s current local space (not world/start space).
 *
 * Date: 2025-08-31 (AEST)
 * Note: Original base code remains subject to the Oculus SDK License Agreement.
 */



/*
 * NonUniformGrabFreeTransformer
 * Based on Meta's GrabFreeTransformer (Interaction SDK v78).
 *
 * GOAL: Keep position & rotation behavior IDENTICAL to the base transformer,
 * while adding optional NON-UNIFORM (per-axis) scaling and quality-of-life toggles.
 *
 * -------------------- DIFF SUMMARY vs BASE --------------------
 * 1) NEW inspector toggles:
 *    - _uniformScaling (uniform vs. per-axis scaling)            // DIFFERENCE FROM BASE
 *    - _lockRotationWhileScaling (freeze rotation with 2 hands)  // DIFFERENCE FROM BASE
 *    - _limitScalingToTwoMostParallelAxes (plane-only scaling)   // DIFFERENCE FROM BASE
 *
 * 2) SCALE path:
 *    - Uniform path: identical to base (UpdateScale)             // SAME AS BASE
 *    - Non-uniform path: UpdateScalePerAxis(...) computes per-axis factors
 *      in the OBJECT'S CURRENT local space (not start/world).    // DIFFERENCE FROM BASE
 *    - Optional two-axis plane mask driven by current grab dir.  // DIFFERENCE FROM BASE
 *
 * 3) ROTATION path:
 *    - Identical to base unless _lockRotationWhileScaling is on. // DIFFERENCE FROM BASE
 *
 * Position path is unchanged.
 */

using System.Buffers;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction
{
    public class NonUniformGrabFreeTransformer : MonoBehaviour, ITransformer
    {
        // -------- NEW TOGGLES (not in base) ---------------------------------
        [SerializeField]
        [Tooltip("If true, uses the original uniform scaling. If false, uses per-axis (non-uniform) scaling.")]
        private bool _uniformScaling = true;  // DIFFERENCE FROM BASE

        [SerializeField]
        [Tooltip("If true, freezes rotation while two hands are grabbing (scaling only). One-hand rotation is unaffected.")]
        private bool _lockRotationWhileScaling = false;  // DIFFERENCE FROM BASE

        [SerializeField]
        [Tooltip("When non-uniform is enabled, restrict scaling to the two axes most parallel to the current grab direction (the third axis is held at 1x).")]
        private bool _limitScalingToTwoMostParallelAxes = false;  // DIFFERENCE FROM BASE
        // ---------------------------------------------------------------------

        [SerializeField]
        [Tooltip("Constrains the position of the object along different axes. Units are meters.")]
        private TransformerUtils.PositionConstraints _positionConstraints =
            new TransformerUtils.PositionConstraints()
            {
                XAxis = new TransformerUtils.ConstrainedAxis(),
                YAxis = new TransformerUtils.ConstrainedAxis(),
                ZAxis = new TransformerUtils.ConstrainedAxis()
            };

        [SerializeField]
        [Tooltip("Constrains the rotation of the object along different axes. Units are degrees.")]
        private TransformerUtils.RotationConstraints _rotationConstraints =
            new TransformerUtils.RotationConstraints()
            {
                XAxis = new TransformerUtils.ConstrainedAxis(),
                YAxis = new TransformerUtils.ConstrainedAxis(),
                ZAxis = new TransformerUtils.ConstrainedAxis()
            };

        [SerializeField]
        [Tooltip("Constrains the local scale of the object along different axes. Expressed as a scale factor.")]
        private TransformerUtils.ScaleConstraints _scaleConstraints =
            new TransformerUtils.ScaleConstraints()
            {
                ConstraintsAreRelative = true,
                XAxis = new TransformerUtils.ConstrainedAxis()
                {
                    ConstrainAxis = true,
                    AxisRange = new TransformerUtils.FloatRange() { Min = 1, Max = 1 }
                },
                YAxis = new TransformerUtils.ConstrainedAxis()
                {
                    ConstrainAxis = true,
                    AxisRange = new TransformerUtils.FloatRange() { Min = 1, Max = 1 }
                },
                ZAxis = new TransformerUtils.ConstrainedAxis()
                {
                    ConstrainAxis = true,
                    AxisRange = new TransformerUtils.FloatRange() { Min = 1, Max = 1 }
                },
            };

        [SerializeField]
        [Tooltip("If enabled, breaks the \"grab point\" when scale is constrained so reversing the motion immediately scales instead of waiting to re-cross the prior grab point.")]
        private bool _resetScaleResponsivenessOnConstraintOvershoot = false;

        private IGrabbable _grabbable;
        private Pose _grabDeltaInLocalSpace;
        private TransformerUtils.PositionConstraints _relativePositionConstraints;
        private TransformerUtils.ScaleConstraints _relativeScaleConstraints;

        private Quaternion _lastRotation = Quaternion.identity;
        private Vector3 _lastScale = Vector3.one;

        private GrabPointDelta[] _deltas;

        // ---------- Internal data structure (copied from base) ----------
        internal struct GrabPointDelta
        {
            private const float _epsilon = 0.000001f;

            public Vector3 PrevCentroidOffset { get; private set; } // world
            public Vector3 CentroidOffset     { get; private set; } // world

            public Quaternion PrevRotation { get; private set; }
            public Quaternion Rotation     { get; private set; }

            public GrabPointDelta(Vector3 centroidOffset, Quaternion rotation)
            {
                PrevCentroidOffset = CentroidOffset = centroidOffset;
                PrevRotation = Rotation = rotation;
            }

            public void UpdateData(Vector3 centroidOffset, Quaternion rotation)
            {
                PrevCentroidOffset = CentroidOffset;
                CentroidOffset = centroidOffset;

                PrevRotation = Rotation;

                // Keep quaternion sign consistent
                if (Quaternion.Dot(rotation, Rotation) < 0)
                {
                    rotation.x = -rotation.x;
                    rotation.y = -rotation.y;
                    rotation.z = -rotation.z;
                    rotation.w = -rotation.w;
                }
                Rotation = rotation;
            }

            public bool IsValidAxis() => CentroidOffset.sqrMagnitude > _epsilon;
        }

        // ---------- ITransformer (same structure as base) ----------
        public void Initialize(IGrabbable grabbable)
        {
            _grabbable = grabbable;
            _relativePositionConstraints =
                TransformerUtils.GenerateParentConstraints(_positionConstraints, _grabbable.Transform.localPosition);
            _relativeScaleConstraints =
                TransformerUtils.GenerateParentConstraints(_scaleConstraints, _grabbable.Transform.localScale);
        }

        public void BeginTransform()
        {
            int count = _grabbable.GrabPoints.Count;

            _deltas = ArrayPool<GrabPointDelta>.Shared.Rent(count);

            InitializeDeltas(count, _grabbable.GrabPoints, ref _deltas);
            Vector3 centroid = GetCentroid(_grabbable.GrabPoints);

            Transform targetTransform = _grabbable.Transform;
            _grabDeltaInLocalSpace = new Pose(
                targetTransform.InverseTransformVector(centroid - targetTransform.position),
                targetTransform.rotation);

            _lastRotation = Quaternion.identity;
            _lastScale = targetTransform.localScale;
        }

        public void UpdateTransform()
        {
            int count = _grabbable.GrabPoints.Count;
            Transform targetTransform = _grabbable.Transform;

            Vector3 localPosition = UpdateTransformerPointData(_grabbable.GrabPoints, ref _deltas);

            // ---------------- SCALE ----------------
            if (count <= 1)
            {
                _lastScale = targetTransform.localScale; // no scale change with one hand
            }
            else
            {
                if (_uniformScaling)
                {
                    // Uniform factor: identical to base
                    float s = UpdateScale(count, _deltas);            // SAME AS BASE
                    _lastScale = s * _lastScale;
                }
                else
                {
                    // Per-axis factor in CURRENT object local space       // DIFFERENCE FROM BASE
                    Vector3 sv = UpdateScalePerAxis(count, _deltas, targetTransform.rotation);

                    // Optional: keep only the two axes most parallel to grab dir  // DIFFERENCE FROM BASE
                    if (_limitScalingToTwoMostParallelAxes)
                    {
                        Vector3 mask = ComputeTwoAxisMask(_grabbable.GrabPoints, targetTransform.rotation);
                        sv = new Vector3(mask.x > 0 ? sv.x : 1f,
                                         mask.y > 0 ? sv.y : 1f,
                                         mask.z > 0 ? sv.z : 1f);
                    }

                    _lastScale = Vector3.Scale(sv, _lastScale);
                }
            }

            targetTransform.localScale =
                TransformerUtils.GetConstrainedTransformScale(_lastScale, _relativeScaleConstraints);

            if (_resetScaleResponsivenessOnConstraintOvershoot)
            {
                _lastScale = targetTransform.localScale;
            }

            // ---------------- ROTATION ----------------
            // Identical to base unless locking is enabled with 2 hands        // DIFFERENCE FROM BASE
            bool twoHand = count > 1;
            if (!(twoHand && _lockRotationWhileScaling))
            {
                _lastRotation = UpdateRotation(count, _deltas) * _lastRotation; // SAME AS BASE
            }
            // else: freeze _lastRotation while two-hand, preserving any prior one-hand rotation

            Quaternion rotation = _lastRotation * _grabDeltaInLocalSpace.rotation;
            targetTransform.rotation = TransformerUtils.GetConstrainedTransformRotation(
                rotation, _rotationConstraints, targetTransform.parent);

            // ---------------- POSITION ----------------
            // Identical to base
            Vector3 position = localPosition - targetTransform.TransformVector(_grabDeltaInLocalSpace.position);
            targetTransform.position = TransformerUtils.GetConstrainedTransformPosition(
                position, _relativePositionConstraints, targetTransform.parent);
        }

        public void EndTransform()
        {
            ArrayPool<GrabPointDelta>.Shared.Return(_deltas);
            _deltas = null;
        }

        // ---------- Helpers (copied from base unless noted) ----------
        internal static void InitializeDeltas(int count, List<Pose> poses, ref GrabPointDelta[] deltas)
        {
            Vector3 centroid = GetCentroid(poses);
            for (int i = 0; i < count; i++)
            {
                Vector3 centroidOffset = GetCentroidOffset(poses[i], centroid);
                deltas[i] = new GrabPointDelta(centroidOffset, poses[i].rotation);
            }
        }

        internal static Vector3 UpdateTransformerPointData(List<Pose> poses, ref GrabPointDelta[] deltas)
        {
            Vector3 centroid = GetCentroid(poses);
            for (int i = 0; i < poses.Count; i++)
            {
                Vector3 centroidOffset = GetCentroidOffset(poses[i], centroid);
                deltas[i].UpdateData(centroidOffset, poses[i].rotation);
            }
            return centroid;
        }

        internal static Vector3 GetCentroid(List<Pose> poses)
        {
            int count = poses.Count;
            Vector3 sumPosition = Vector3.zero;
            for (int i = 0; i < count; i++)
            {
                Pose pose = poses[i];
                sumPosition += pose.position;
            }
            return sumPosition / count;
        }

        internal static Vector3 GetCentroidOffset(Pose pose, Vector3 centre)
        {
            Vector3 centroidOffset = centre - pose.position;
            return centroidOffset;
        }

        internal static Quaternion UpdateRotation(int count, GrabPointDelta[] deltas)
        {
            // EXACT copy of base rotation logic
            Quaternion combinedRotation = Quaternion.identity;

            float fraction = 1f / count;
            for (int i = 0; i < count; i++)
            {
                GrabPointDelta data = deltas[i];

                Quaternion rotDelta = data.Rotation * Quaternion.Inverse(data.PrevRotation);

                if (data.IsValidAxis())
                {
                    Vector3 aimingAxis = data.CentroidOffset.normalized;
                    Quaternion dirDelta = Quaternion.FromToRotation(data.PrevCentroidOffset.normalized, aimingAxis);
                    combinedRotation = Quaternion.Slerp(Quaternion.identity, dirDelta, fraction) * combinedRotation;

                    rotDelta.ToAngleAxis(out float angle, out Vector3 axis);
                    float projectionFactor = Vector3.Dot(axis, aimingAxis);
                    rotDelta = Quaternion.AngleAxis(angle * projectionFactor, aimingAxis);
                }

                combinedRotation = Quaternion.Slerp(Quaternion.identity, rotDelta, fraction) * combinedRotation;
            }

            return combinedRotation;
        }

        // ---------- Scale (uniform) — SAME AS BASE ----------
        internal static float UpdateScale(int count, GrabPointDelta[] deltas)
        {
            float scaleDelta = 0f;
            for (int i = 0; i < count; i++)
            {
                GrabPointDelta data = deltas[i];
                if (data.IsValidAxis())
                {
                    float factor = Mathf.Sqrt(
                        data.CentroidOffset.sqrMagnitude / data.PrevCentroidOffset.sqrMagnitude);
                    scaleDelta += factor / count;
                }
                else
                {
                    scaleDelta += 1f / count;
                }
            }
            return scaleDelta;
        }

        // ---------- Scale (non-uniform per-axis in CURRENT local space) ----------
        // DIFFERENCE FROM BASE: this function computes component-wise scale factors
        // by projecting centroid offsets into the object's CURRENT local basis.
        internal static Vector3 UpdateScalePerAxis(int count, GrabPointDelta[] deltas, Quaternion objectRotation)
        {
            Vector3 scaleDelta = Vector3.zero;
            Quaternion invRot = Quaternion.Inverse(objectRotation);

            for (int i = 0; i < count; i++)
            {
                GrabPointDelta data = deltas[i];

                if (data.IsValidAxis())
                {
                    Vector3 currLocal = invRot * data.CentroidOffset;
                    Vector3 prevLocal = invRot * data.PrevCentroidOffset;

                    float fx = SafeAxisRatio(currLocal.x, prevLocal.x);
                    float fy = SafeAxisRatio(currLocal.y, prevLocal.y);
                    float fz = SafeAxisRatio(currLocal.z, prevLocal.z);

                    scaleDelta += new Vector3(fx, fy, fz) * (1f / count);
                }
                else
                {
                    scaleDelta += Vector3.one * (1f / count);
                }
            }

            return scaleDelta;
        }

        private static float SafeAxisRatio(float curr, float prev)
        {
            float ap = Mathf.Abs(prev);
            if (ap < 1e-8f) return 1f; // neutral if previous axis nearly zero
            float r = Mathf.Abs(curr) / ap;
            if (!float.IsFinite(r)) return 1f;
            return Mathf.Clamp(r, 0.01f, 100f);
        }

        // ---------- Plane selection mask (CURRENT local space) ----------
        // DIFFERENCE FROM BASE: used only when non-uniform + plane limit is enabled.
        private static Vector3 ComputeTwoAxisMask(List<Pose> grabPoints, Quaternion objectRotation)
        {
            Vector3 weights;

            if (grabPoints.Count >= 2)
            {
                // Hand-span direction (B - A) in current local space
                Vector3 v = grabPoints[1].position - grabPoints[0].position;
                Vector3 local = Quaternion.Inverse(objectRotation) * v;
                weights = new Vector3(Mathf.Abs(local.x), Mathf.Abs(local.y), Mathf.Abs(local.z));
            }
            else
            {
                // Fallback: sum centroid-offsets projected to current local space
                Vector3 centroid = GetCentroid(grabPoints);
                Vector3 sum = Vector3.zero;
                Quaternion invRot = Quaternion.Inverse(objectRotation);
                for (int i = 0; i < grabPoints.Count; i++)
                {
                    Vector3 offLocal = invRot * (centroid - grabPoints[i].position);
                    sum += new Vector3(Mathf.Abs(offLocal.x), Mathf.Abs(offLocal.y), Mathf.Abs(offLocal.z));
                }
                weights = sum;
            }

            // Drop the smallest component → keep the two most-parallel axes
            int drop = 0;
            if (weights.y < weights.x) drop = 1;
            if (weights.z < weights[drop]) drop = 2;

            Vector3 mask = Vector3.one;
            if (drop == 0) mask.x = 0f;
            else if (drop == 1) mask.y = 0f;
            else mask.z = 0f;

            return mask; // 1 = allowed to scale, 0 = locked
        }

        // ---------- Inject (kept for parity with base) ----------
        public void InjectOptionalPositionConstraints(TransformerUtils.PositionConstraints constraints)
        {
            _positionConstraints = constraints;
        }

        public void InjectOptionalRotationConstraints(TransformerUtils.RotationConstraints constraints)
        {
            _rotationConstraints = constraints;
        }

        public void InjectOptionalScaleConstraints(TransformerUtils.ScaleConstraints constraints)
        {
            _scaleConstraints = constraints;
        }
    }
}
