using UnityEngine;

public class SimpleWASDController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 80f;
    
    public Rigidbody rb;
    public Transform cameraTransform;
    public bool isGrounded;
    public float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        
        cameraTransform = Camera.main.transform;

    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        transform.Rotate(Vector3.up * mouseX);
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

    }
    
    void FixedUpdate()
    {
        float horizontal = 0f;
        float vertical = 0f;
        
        if (Input.GetKey(KeyCode.W)) vertical += 1f;
        if (Input.GetKey(KeyCode.S)) vertical -= 1f;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
        if (Input.GetKey(KeyCode.D)) horizontal += 1f;
        
       
        Vector3 movement = transform.right * horizontal + transform.forward * vertical;
        movement = movement.normalized * walkSpeed;
        
        Vector3 targetVelocity = movement;
        targetVelocity.y = rb.velocity.y;
        
        rb.velocity = targetVelocity;
    }
    
    
    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground (you can adjust this check)
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
    }
    
    void OnCollisionExit(Collision collision)
    {
        // Check if we're no longer touching the ground
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = false;
        }
    }
}
