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
    public AudioSource audio;
    public AudioClip [] clips;

    private float gravity;
    private float jumpVelocity;
    private Vector3 velocity;
    private float velocityXSmoothing;
    private bool facingRight = true;
    public bool doubleJump = false;
    private Controller2D controller;
    private Animator animator;
    private FloatTimer inputScale;
    private FloatTimer accelerationTimeAirborne;
    private FloatTimer accelerationTimeGrounded;
    private BoolTimer dashOnCooldown;
    private BoxCollider2D collider;
    private int platforms = 1;

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
        collider = GetComponent<BoxCollider2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;

        inputScale = gameObject.AddComponent<FloatTimer>().Constructor(1f);
        accelerationTimeAirborne = gameObject.AddComponent<FloatTimer>().Constructor(baseAccelerationTimeAirborne);
        accelerationTimeGrounded = gameObject.AddComponent<FloatTimer>().Constructor(baseAccelerationTimeGrounded);
        dashOnCooldown = gameObject.AddComponent<BoolTimer>().Constructor(false);
    }

    public void UpdateJumpHeight(float jumpHeight) {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
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
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical"));
        if (input.x > 0 && !facingRight)
        {
            facingRight = true;
            transform.localScale = new Vector3(.5f, .5f, .5f);
        }

        if (input.x < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(-.5f, .5f, .5f);
            //transform.position = new Vector3(transform.position.x  - 0.08f, transform.position.y, transform.position.z);
        }

        // Smooth the x velocity
        float targetVelocityX = input.x * moveSpeed * inputScale.Value;
        //float targetVelocityX = 0f;


        if (!dashOnCooldown.Value && Mathf.Abs(input.x) > 0 || controller.collisions.below) {
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded.Value : accelerationTimeAirborne.Value);

        }
        
        // Handle Dashing
        /*
        if (Input.GetButtonDown("Dash") && !dashOnCooldown.Value)
        {
            velocity.x = (facingRight) ? jumpVelocity * 1f : -jumpVelocity * 1f;
            velocity.y = jumpVelocity * 0.5f;
            //accelerationTimeAirborne.UpdateValue(.25f, 1f);
            //accelerationTimeGrounded.UpdateValue(.25f, 1f);
            inputScale.UpdateValue(.25f, .25f);
            dashOnCooldown.UpdateValue(.5f, true);

        }
        */
        // Handle jumps
        if (Input.GetButtonDown("Jump")) {
            if (controller.collisions.below) // Regular jump
            {
                audio.clip = clips[0];
                audio.Play();
                velocity.y = jumpVelocity;
            } else if (controller.collisions.left) { // Left wall jump
                audio.clip = clips[0];
                audio.Play();
                velocity.y = jumpVelocity;
                velocity.x = jumpVelocity / 1.5f;
                inputScale.UpdateValue(.1f, 0);
            } else if (controller.collisions.right) { // Right wall jump
                audio.clip = clips[0];
                audio.Play();
                velocity.y = jumpVelocity;
                velocity.x = -jumpVelocity / 1.5f;
                inputScale.UpdateValue(.1f, 0);
            } else if (!doubleJump) {    // Double jump
                audio.clip = clips[0];
                audio.Play();
                velocity.y = jumpVelocity;
                doubleJump = true;
            }
        }
        // Ignore platform if pressing down
        controller.ignorePlatform = input.y < 0;

        // Add gravity
        velocity.y += gravity * Time.deltaTime;

        /*
        if (input.x > 0 && controller.collisions.right && !Physics2D.Raycast(collider.bounds.max + Vector3.up * 0.1f, Vector2.right, 0.015625f)) {
            velocity = Vector2.zero;
            velocity.y = 4f;

        }
        if (input.x < 0 && controller.collisions.left && !Physics2D.Raycast(collider.bounds.max - new Vector3(collider.bounds.size.x, 0) + Vector3.up * 0.1f, Vector2.left, 0.015625f * 2f)) {
            velocity = Vector2.zero;
            velocity.y = 4f;

        }
        */
        // Finally, send movement velocity to controller
        controller.Move(velocity * Time.deltaTime);

        // Handle platform placement
        if (Input.GetButtonDown("Platform") && platforms > 0)
        {
            Vector3 offset = velocity / moveSpeed + Vector3.down;
            //Vector3 offset = (transform.localScale * Vector2.right * 3 + Vector2.up) * 2;
            GameObject newPlatform = Instantiate(platform, transform.position + offset, Quaternion.identity, DynamicPlatformContainer.instance.transform);
            DynamicPlatformContainer.instance.ValidateCount();
            platforms--;
        }

        // Handle attacking
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    public void Set(string toSet, bool setting, int argc, object[] argv)
    {
        switch (toSet)
        {
            case "HighJump":
                {
                    UpdateJumpHeight((float)argv[0]);
                    return;
                }
            default:
                return;

        }
    }

}

