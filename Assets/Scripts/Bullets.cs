using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    // Grouping bullet-related settings
    [Header("Bullet Settings")]
    [SerializeField] private float bulletSpeed = 20f;    // Speed of the bullet
    [SerializeField] private PlayerMovement player;      // Reference to the PlayerMovement script

    private Rigidbody2D myRigidbody;                      // Rigidbody for bullet movement
    private float xSpeed;                                // Bullet's movement speed based on player orientation

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();   // Get Rigidbody2D component

        // Ensure the player reference is assigned
        if (player == null)
        {
            // Try to find the player dynamically if not set in the Inspector
            player = FindObjectOfType<PlayerMovement>();
        }

        // Set bullet speed based on player's orientation (scale.x) and bullet speed
        if (player != null)
        {
            xSpeed = player.transform.localScale.x * bulletSpeed;
        }
        else
        {
            Debug.LogError("PlayerMovement script is not assigned or found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update bullet's velocity based on xSpeed
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    // Trigger event for collisions with objects tagged "Enemy"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);  // Destroy enemy on hit
        }
        Destroy(gameObject);  // Destroy the bullet
    }

    // Handle collision with other objects (such as walls)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);  // Destroy the bullet on collision
    }
}
