using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables to control movement and jumping
    public float moveSpeed = 5f;       
    public float jumpForce = 7f;        
    private bool isGrounded = false;    

    // Reference to Rigidbody for applying physics
    private Rigidbody2D rb;             // For 2D games, use Rigidbody2D. For 3D use Rigidbody

    // Key binding variables for custom controls
    public KeyCode moveLeftKey = KeyCode.A;   
    public KeyCode moveRightKey = KeyCode.D;  
    public KeyCode jumpKey = KeyCode.Space;   

    void Start()
    {
        // Getting the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody2D>();    // For 2D. Use Rigidbody if 3D.
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = 0f;

        // Move left
        if (Input.GetKey(moveLeftKey))
        {
            moveInput = -1f;
        }

        // Move right
        if (Input.GetKey(moveRightKey))
        {
            moveInput = 1f;
        }

        // Apply movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Jumping logic
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);  // Jump
            isGrounded = false;  // The player is no longer grounded once they jump
        }
    }

    // Check if the player is on the ground using collision detection
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
