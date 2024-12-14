using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Grouping movement and physics settings
    [Header("Enemy Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;  // Movement speed of the enemy
    private Rigidbody2D myRiggedbody;               // Rigidbody2D component for enemy's physics

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Rigidbody2D component
        myRiggedbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set the enemy's velocity based on moveSpeed
        myRiggedbody.velocity = new Vector2(moveSpeed, 0f);
    }

    // Trigger event when an object exits the collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reverse the direction of movement when the enemy exits the trigger
        moveSpeed = -moveSpeed;

        // Flip the enemy's facing direction
        FlipFacing();
    }

    // Method to flip the enemy's facing direction
    void FlipFacing()
    {
        // Flip the sprite's x-scale based on the velocity's sign
        transform.localScale = new Vector2(-(Mathf.Sign(myRiggedbody.velocity.x)), 1f);
    }
}
