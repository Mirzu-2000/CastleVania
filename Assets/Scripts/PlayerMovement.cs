using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Serialized fields for adjustable movement parameters in the Inspector
    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private float impulseDeadForce = 5f;

    [Header("Bullet and Gun Settings")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;

    [Header("Restart Settings")]
    [SerializeField] private float restartDelay = 0.5f;
    [SerializeField] private LevelManager levelManager;

    [Header("Player SFX")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private AudioClip diedSFX;

    // Cached references to components
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;

    // For climbing
    private float gravityScaleAtStart;

    // For checking death
    private bool isAlive = true;

    // Input tracking
    private Vector2 moveInput;

    private void Start()
    {
        // Initialize component references
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        Run();
        FlipPlayer();
        Climbing();
        Die();
    }

    // Handle player death
    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Danger")))
        {
            isAlive = false;
            DeadForce();
            myAnimator.SetTrigger("Dying");
            AudioSource.PlayClipAtPoint(diedSFX, myRigidbody.transform.position);
            StartCoroutine(RestartDelay());
        }
    }

    private IEnumerator RestartDelay()
    {
        yield return new WaitForSecondsRealtime(restartDelay);
        levelManager.RestartLevel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Level Exit"))
        {
            Debug.Log("Level Exit");
            levelManager.LoadNextLevel();
        }
        else if (other.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("GameOver");
            levelManager.ShowGameOverPanel();
        }
    }

    private void DeadForce()
    {
        myRigidbody.AddForce(Vector3.up * impulseDeadForce, ForceMode2D.Impulse);
    }

    private void OnFire(InputValue value)
    {
        if (!isAlive) { return; }

        AudioSource.PlayClipAtPoint(laserSFX, myRigidbody.transform.position);
        Instantiate(bullet, gun.position, gun.rotation);
    }

    private void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            AudioSource.PlayClipAtPoint(jumpSFX, myRigidbody.transform.position);
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    private void Run()
    {
        if (!isAlive) { return; }

        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasVelocity = Mathf.Abs(playerVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasVelocity);
    }

    private void FlipPlayer()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void Climbing()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;

        bool playerHasClimbingVelocity = Mathf.Abs(climbVelocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasClimbingVelocity);
    }
}
