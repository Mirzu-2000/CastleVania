
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D myRigidbody;
    [SerializeField] PlayerMovement player; 
    float xSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        // Check if the player reference is not assigned in the Inspector
        if (player == null)
        {
            // Try to find the player GameObject dynamically
            player = FindObjectOfType<PlayerMovement>();
        }

        // Ensure player is found before accessing its transform
        if (player != null)
        {
            xSpeed = player.transform.localScale.x * bulletSpeed;
        }
        else
        {
            Debug.LogError("PlayerMovement script is not assigned or found!");
        }
    }

    void Update()
    {
        // Ensure bullet moves with the correct speed
        myRigidbody.velocity = new Vector2(xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
