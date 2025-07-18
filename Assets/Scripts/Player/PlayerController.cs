using UnityEngine;
using UnityEngine.Rendering.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //=====================OPTION 2========================

        //TryGetComponent<Rigidbody2D>(out rb);
        //  if (rb == null)
        //  {
        //      Debug.LogError("Rigidbody2D component not found on PlayerController.");
        //  }

        //=====================OPTION 2========================

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on PlayerController.");
        }
        else
        {
            Debug.Log($"Rigidbody2D exists on {rb.name}");
        }

        //=====================OPTION 3========================

        //rb = GetComponent<Rigidbody2D>();

        //if (rb != null) Debug.Log($"Rigidbody2D Existx {rb.name}.");
    }

    // Update is called once per frame
    void Update()
    {
        //Raw input is used to get the input value without any smoothing or filtering
        //This is useful for precise control, especially in platformers or fast-paced games
        float hValue = Input.GetAxisRaw("Horizontal");
        rb.linearVelocityX = hValue * 5f;

        if (Input.GetButtonDown("Jump")) 
        {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse); // AddForce applies a force to the Rigidbody2D
        }
    }
}
