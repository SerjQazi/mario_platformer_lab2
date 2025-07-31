using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))] // Ensure the GameObject has these components
public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool isGrounded = false; // Flag to check if the player is on the ground
    private LayerMask groundLayer; // LayerMask to define which layers are considered ground

    [SerializeField] private float groundCheckRadius = 0.02f; // Radius for ground checking, used in OverlapCircle

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Collider2D col; // Collider to check for ground collision
    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the GameObject
        col = GetComponent<Collider2D>(); // Get the Collider2D component to check for ground collision



        // Initialize the groundLayer to include the "Ground" layer
        groundLayer = LayerMask.GetMask("Ground"); // Get the layer mask for the "Ground" layer


        // Check if the groundLayer is set correctly
        if (groundLayer == 0)
        {
            Debug.LogWarning("Ground layer not set or does not exist. Please create a layer named 'Ground' and assign it to the ground objects.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Handle player input and movement
        // Get horizontal input and apply it to the Rigidbody2D's velocity
        float hValue = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = hValue * 5f;

        SpriteFlip(hValue); // Call the method to flip the sprite based on horizontal input

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); // AddForce applies a force to the Rigidbody2D
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
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
