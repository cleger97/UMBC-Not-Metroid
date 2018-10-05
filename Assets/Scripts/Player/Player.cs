using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public static Player instance;
    public float jumpHeight = 3;
    public float timeToJumpApex = .4f;
    public float baseAccelerationTimeAirborne = .15f;
    public float baseAccelerationTimeGrounded = .05f;
    public float moveSpeed = 8;
    public GameObject platform;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private bool facingRight = true;
    private bool doubleJump = false;
    private Controller2D controller;
    private Animator animator;
    private FloatTimer inputScale;
    private FloatTimer accelerationTimeAirborne;
    private FloatTimer accelerationTimeGrounded;
    private BoolTimer dashOnCooldown;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        } else {
            DontDestroyOnLoad(this);
            instance = this;
        }
        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        inputScale = gameObject.AddComponent<FloatTimer>().Constructor(1f);
        accelerationTimeAirborne = gameObject.AddComponent<FloatTimer>().Constructor(baseAccelerationTimeAirborne);
        accelerationTimeGrounded = gameObject.AddComponent<FloatTimer>().Constructor(baseAccelerationTimeGrounded);
        dashOnCooldown = gameObject.AddComponent<BoolTimer>().Constructor(false);
    }

    void Update()
    {
        // Reset velocity if collision above or below
        // Reset double jump if collision below
        if (controller.collisions.above)
        {
            velocity.y = 0;
        }
        if (controller.collisions.below)
        {
            velocity.y = 0;
            doubleJump = false;
        }

        // Get input and flip direction if necessary
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input.x > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (input.x < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Smooth the x velocity
        float targetVelocityX = input.x * moveSpeed * inputScale.Value;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded.Value : accelerationTimeAirborne.Value);

        // Handle Dashing
        if (Input.GetButtonDown("Dash") && !dashOnCooldown.Value)
        {
            velocity.x = (facingRight) ? jumpVelocity * 1.25f : -jumpVelocity * 1.25f;

            accelerationTimeAirborne.UpdateValue(.25f, 1f);
            accelerationTimeGrounded.UpdateValue(.25f, 1f);
            inputScale.UpdateValue(.25f, .25f);
            dashOnCooldown.UpdateValue(.5f, true);

        }

        // Handle jumps
        if (Input.GetButtonDown("Jump")) {
            if (controller.collisions.below) // Regular jump
            {
                velocity.y = jumpVelocity;
                accelerationTimeAirborne.UpdateValue(.25f, baseAccelerationTimeAirborne);
                accelerationTimeGrounded.UpdateValue(.25f, baseAccelerationTimeGrounded);
            } else if (controller.collisions.left) { // Left wall jump
                velocity.y = jumpVelocity;
                velocity.x = jumpVelocity / 4f;
                accelerationTimeAirborne.UpdateValue(.25f, 1f);
                accelerationTimeGrounded.UpdateValue(.25f, 1f);
            } else if (controller.collisions.right) { // Right wall jump
                velocity.y = jumpVelocity;
                velocity.x = -jumpVelocity / 4f;
                accelerationTimeAirborne.UpdateValue(.25f, 1f);
                accelerationTimeGrounded.UpdateValue(.25f, 1f);
            } else if (!doubleJump) {    // Double jump
                velocity.y = jumpVelocity;
                doubleJump = true;
            }
        }
        // Ignore platform if pressing down
        controller.ignorePlatform = input.y < 0;

        // Add gravity
        velocity.y += gravity * Time.deltaTime;

        // Finally, send movement velocity to controller
        controller.Move(velocity * Time.deltaTime);

        // Handle platform placement
        if (Input.GetButtonDown("Platform"))
        {
            Vector3 offset = velocity / moveSpeed + Vector3.down;
            GameObject newPlatform = Instantiate(platform, transform.position + offset, Quaternion.identity, DynamicPlatformContainer.instance.transform);
            DynamicPlatformContainer.instance.ValidateCount();
        }

        // Handle attacking
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

}

