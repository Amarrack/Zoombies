using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    // The speed at which the character moves
    public float moveSpeed = 50f;

    // The force at which the character jumps
    public float jumpForce = 5f;

    // The speed at which the character sprints
    public float sprintSpeed = 150f;

    // The height of the vaultable objects
    public float vaultHeight = 1f;

    // The force at which the character vaults
    public float vaultForce = 5f;

    // A reference to the character's rigidbody
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get a reference to the character's rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the character's movement direction
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Normalize the movement direction to prevent diagonal movement from being faster
        movement = movement.normalized;

        // Check if the character is in contact with the ground
        bool isGrounded = Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Ground"));

        // Check if the character is pressing the vault button and is within range of a vaultable object
        if (Input.GetButtonDown("Jump") && Physics.Raycast(transform.position, movement, out RaycastHit hit, 1f, LayerMask.GetMask("Vaultable")))
        {
            // Check if the object is tall enough to vault over
            if (hit.collider.bounds.max.y >= transform.position.y + vaultHeight)
            {
                // Calculate the direction and distance of the vault
                Vector3 vaultDirection = hit.point - transform.position + movement;
                float vaultDistance = Vector3.Distance(hit.point, transform.position) + 0.5f;

                // Move the character to the starting position of the vault
                rb.MovePosition(transform.position + movement * vaultDistance);

                // Add a force to vault the character over the object
                rb.AddForce(vaultDirection * vaultForce, ForceMode.Impulse);
            }
        }
        else
        {
            // Determine whether the character should sprint or not
            if (Input.GetButton("Sprint"))
            {
                // Move the character at the sprint speed
                rb.MovePosition(transform.position + movement * sprintSpeed * Time.deltaTime);
            }
            else
            {
                // Move the character at the regular speed
                rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
            }
        }

        // Check if the player pressed the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Add a vertical force to the character
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
