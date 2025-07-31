using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the GameObject
        sr = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the GameObject
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
