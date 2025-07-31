using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))] // Ensure the GameObject has these components
public class PlayerController : MonoBehaviour
{
    private bool isGrounded = false; // Flag to check if the player is on the ground
    private LayerMask groundLayer; // LayerMask to define which layers are considered ground

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Collider2D col; // Collider to check for ground collision
    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);


    //private Vector2 groundCheckPos;
    //Vector2 GetGroundCheckPos()
    //{         // Calculate the position for ground checking based on the player's position and the collider's bounds
    //          //return new Vector2(transform.position.x, col.bounds.min.y);

    //    /*This method returns the position to check for ground collision
    //      It uses the collider's bounds to determine the position at the bottom of the collider
    //      This is useful for checking if the player is grounded by casting a ray downwards from this position
    //      Return the position at the bottom of the collider's bounds*/

    //    return new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);
    //}



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the GameObject
        col = GetComponent<Collider2D>(); // Get the Collider2D component to check for ground collision



        // Initialize the groundLayer to include the "Ground" layer
        groundLayer = LayerMask.GetMask("Ground"); // Get the layer mask for the "Ground" layer
    }

    // Update is called once per frame
    void Update()
    {
        //Raw input is used to get the input value without any smoothing or filtering
        //This is useful for precise control, especially in platformers or fast-paced games
        float hValue = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = hValue * 5f;

        SpriteFlip(hValue); // Call the method to flip the sprite based on horizontal input

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); // AddForce applies a force to the Rigidbody2D
        }

        //// Check if the player is grounded by casting a ray downwards from the groundCheckPos
        //isGrounded = Physics2D.Raycast(groundCheckPos, Vector2.down, 0.1f, groundLayer);
        //// Debug.DrawRay(groundCheckPos, Vector2.down * 0.1f, Color.red); // Optional: Draw a ray in the editor for debugging
        // Debug.Log($"Grounded: {isGrounded}"); // Optional: Log the grounded state for debugging
    }



    void SpriteFlip(float hValue)
    {
        // Flip the sprite based on the horizontal input
        //if (hValue < 0)
        //{
        //    sr.flipX = true; // Flip the sprite to face left
        //}
        //else if (hValue > 0)
        //{
        //    sr.flipX = false; // Flip the sprite to face right
        //}

        // Simplified sprite flipping logic
        //sr.flipX = hValue < 0;
        //if (hValue != 0 ) sr.flipX = hValue < 0; // Flip the sprite based on horizontal input

        // Better, optimized sprite flipping logic
        // If the sprite is flipped and the horizontal input is in the opposite direction, flip it back
        if (hValue > 0 && sr.flipX || hValue < 0 && !sr.flipX)
        {
            sr.flipX = !sr.flipX;

        }

    }
}
