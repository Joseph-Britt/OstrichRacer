using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VehicleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveForce = 10f;
    public float maxSpeed = 5f;
    public float jumpForce = 5f;
    public float rotationSpeed = 10f;

    [Header("References")]
    public Transform cameraTransform;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // We'll handle rotation manually
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        // Jump input (for later)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // we'll probably change to an input system? will hard code for now
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // oops make it camera relative
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;
        camForward.Normalize();
        
        Vector3 camRight = cameraTransform.right;
        camRight.y = 0f;
        camRight.Normalize();
        
        //Add force
        Vector3 move = (camForward * v + camRight * h).normalized;
        rb.AddForce(move * moveForce);
        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
        
        //Basic rotation shi
        if (flatVel.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(flatVel, Vector3.up);
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
        }
    }
}
