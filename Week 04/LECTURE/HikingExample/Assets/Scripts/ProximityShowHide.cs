using System.Collections.Generic;
using UnityEngine;

public class ProximityShowHide : MonoBehaviour
{
    public string targetTag = "point";
    public float showDistance = 5f;
    public List<GameObject> targetObjects = new List<GameObject>();
    public Transform currentTarget;

    void Start()
    {
        GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(targetTag);
        targetObjects.AddRange(foundObjects);


        GameObject player = GameObject.FindGameObjectWithTag("Player");

        currentTarget = player.transform;

    }

    void Update()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj != null)
            {
                float distance = Vector3.Distance(obj.transform.position, currentTarget.position);
                bool shouldShow = distance <= showDistance;
                obj.SetActive(shouldShow);
            }
        }
    }
}
