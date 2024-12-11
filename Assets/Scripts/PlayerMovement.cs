using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Serialized fields for adjustable movement parameters in the Inspector
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float impulseDeadForce = 5f;

    // Cached references to components
    Rigidbody2D myRigidbody; 
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    float gravityScaleAtStart;

    bool isAlive = true;

    // Input tracking
    Vector2 moveInput;

    void Start()
    {
        // Initialize component references
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        // Handle running and flipping logic every frame
        Run();
        FlipPlayer();
        Climbing();
        Die();


    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Diying");
            DeadForce();
        }
    }

    void DeadForce()
    {
        myRigidbody.AddForce(Vector3.up * impulseDeadForce, ForceMode2D.Impulse);

    }


    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }
        // Capture movement input from the player
        moveInput = value.Get<Vector2>();
        //Debug.Log(moveInput); // Log the input values for debugging purposes
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        // Ensure the player can only jump when touching the ground layer
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        // Add vertical velocity to simulate jumping when the input is pressed
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void Run()
    {
        if (!isAlive) { return; }
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

    void Climbing()
    {
        // Ensure the player can only Climbs when touching the Ladder layer
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) 
        {
           myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return; 
        }
        // Calculate player velocity based on input and apply vartical movement
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        //to avoid the player Sliding Dwown of the Ladder.
        myRigidbody.gravityScale = 0f;

        // Check if the player is moving vartically
        bool playerHasClimbingVelocity = Mathf.Abs(climbVelocity.y) > Mathf.Epsilon;

        // Update animator parameter to reflect climbing state
        myAnimator.SetBool("IsClimbing", playerHasClimbingVelocity);
    }

    

}
