using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    // Grouping references for scoring and audio
    [Header("Coin Pickup Settings")]
    [SerializeField] private ScoreManager scoreManager;   // Reference to the ScoreManager for adding score
    [SerializeField] private AudioClip coinPickupSFX;     // Sound effect for coin pickup

    private bool wasCollected = false;  // Flag to track if the coin has already been collected

    // Trigger event when an object enters the coin's collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the coin and if it hasn't been collected yet
        if (other.CompareTag("Player") && !wasCollected)
        {
            wasCollected = true;  // Mark the coin as collected
            scoreManager.AddScore(100);  // Add 100 points to the score

            // Play the coin pickup sound at the camera's position
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            // Destroy the coin object after pickup
            Destroy(gameObject);
        }
    }
}
