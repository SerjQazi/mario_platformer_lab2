using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))] // Ensure the GameObject has these components
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isGrounded = false; // Flag to check if the player is on the ground
    private LayerMask groundLayer; // LayerMask to define which layers are considered ground

    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground checking, used in OverlapCircle

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D col; // Collider to check for ground collision
    private Animator anim; // Animator component for animations

    public float speed = 5f;

    [SerializeField]private int jumpLimit = 2; // Maximum number of jumps allowed (double jump enabled)
    private int jumpCount = 1; // Counter for the number of jumps performed

    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the GameObject
        col = GetComponent<Collider2D>(); // Get the Collider2D component to check for ground collision
        anim = GetComponent<Animator>(); // Get the Animator component for animations
        

        // Initialize the groundLayer to include the "Ground" layer
        groundLayer = LayerMask.GetMask("Ground"); // Get the layer mask for the "Ground" layer


        //// Check if the groundLayer is set correctly
        if (groundLayer == 1)
        {
            Debug.LogWarning("Ground layer not set or does not exist. Please create a layer named 'Ground' and assign it to the ground objects.");
        }
    }

    // Update is called once per frame
    void Update()
    {

        // if right click is pressed, player attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true); // Trigger the attack animation
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("attack", false); // Trigger the shoot animation
        }

        // Handle player input and movement
        // Get horizontal input and apply it to the Rigidbody2D's velocity
        float hValue = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = hValue * speed;



        // if holding shift toggle isRunning true and increase speed to 10f
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", true); // Set the isRunning parameter to true in the animator
            speed = 10f; // Increase speed to 10f when running
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isRunning", false); // Set the isRunning parameter to false in the animator
            speed = 5f;
        }

        SpriteFlip(hValue); // Call the method to flip the sprite based on horizontal input
        
        // Check if the player is grounded using OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && jumpCount < jumpLimit)
        {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); // AddForce applies a force to the Rigidbody2D
            jumpCount++; // Increment the jump count when the player jumps
            //Debug.Log($"Jump Count: {jumpCount}"); // Log the jump count for debugging
        }


        if (isGrounded)
        {
            jumpCount = 1; // Reset the jump count when the player is grounded
            //Debug.Log($"Jump Count: {jumpCount}"); // Log the jump count for debugging

            // update the animator parameters
        }
            anim.SetFloat("hValue", Mathf.Abs(hValue)); // Set the horizontal value for the animator
            anim.SetBool("isGrounded", isGrounded); // Set the grounded state in the animator
    }

    void SpriteFlip(float hValue)
    {
        // If the sprite is flipped and the horizontal input is in the opposite direction, flip it back
        if (hValue > 0 && sr.flipX || hValue < 0 && !sr.flipX)
        {
            sr.flipX = !sr.flipX;

        }        

    }
}
