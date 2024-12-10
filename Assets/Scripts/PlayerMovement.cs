using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Serialized fields for adjustable movement parameters in the Inspector
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;

    // Cached references to components
    Rigidbody2D myRigidbody; 
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider; 

    // Input tracking
    Vector2 moveInput;

    void Start()
    {
        // Initialize component references
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        // Handle running and flipping logic every frame
        Run();
        FlipPlayer();
    }

    void OnMove(InputValue value)
    {
        // Capture movement input from the player
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput); // Log the input values for debugging purposes
    }

    void OnJump(InputValue value)
    {
        // Ensure the player can only jump when touching the ground layer
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        // Add vertical velocity to simulate jumping when the input is pressed
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        // Calculate player velocity based on input and apply horizontal movement
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        // Check if the player is moving horizontally
        bool playerHasVelocity = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;

        // Update animator parameter to reflect running state
        myAnimator.SetBool("IsRunning", playerHasVelocity);
    }

    void FlipPlayer()
    {
        // Determine if the player has significant horizontal movement
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // Flip the player's sprite based on movement direction
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }
}
