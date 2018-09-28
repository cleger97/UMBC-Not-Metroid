
using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public static Player instance;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    public float baseAccelerationTimeAirborne = .2f;
    public float baseAccelerationTimeGrounded = .1f;
    public float moveSpeed = 6;

    public float dashCooldown = 0.5f;

    public GameObject platform;
    public Transform platformContainer;

    public GameObject weapon;

    float inputScale = 1f;

    float accelerationTimeAirborne;
    float accelerationTimeGrounded;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    bool facingRight = true;
    bool dashOnCooldown = false;
    bool doubleJump = false;
    Controller2D controller;
    private Animator animator;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        } else {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
    void Start()
    {

        controller = GetComponent<Controller2D>();
        animator = GetComponent<Animator>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
        accelerationTimeAirborne = baseAccelerationTimeAirborne;
        accelerationTimeGrounded = baseAccelerationTimeGrounded;
    }

    void Update()
    {

        if (controller.collisions.above)
        {
            velocity.y = 0;
        }
        if (controller.collisions.below)
        {
            velocity.y = 0;
            doubleJump = false;
        }
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (controller.collisions.below)
            {
                velocity.y = jumpVelocity;
            } else if (!doubleJump){
                velocity.y = jumpVelocity;
                doubleJump = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.left && !controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            velocity.x = jumpVelocity/3f;
            StartCoroutine(ChangeDrag(.3f, .8f));
        }
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.right && !controller.collisions.below)
        {
            velocity.y = jumpVelocity;
            velocity.x = -jumpVelocity/3f;
            StartCoroutine(ChangeDrag(.3f, .8f));
        }

        float targetVelocityX = input.x * moveSpeed * inputScale;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        if (Input.GetButtonDown("Dash"))
        {
            if (facingRight)
            {
                velocity.x = jumpVelocity;

            }
            else
            {
                velocity.x = -jumpVelocity;
            }
            StartCoroutine(ChangeDrag(.2f, .8f));
            StartCoroutine(SetInputScale(.3f, .1f));
            StartCoroutine(DashCooldown());
        }
        controller.ignorePlatform = input.y < 0;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Platform"))
        {
            Vector3 offset = velocity / moveSpeed + Vector3.down;
            GameObject newPlatform = Instantiate(platform, transform.position + offset, Quaternion.identity, platformContainer.transform);
            platformContainer.GetComponent<DynamicPlatformContainer>().ValidateCount();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack");
        }
    }

    private IEnumerator ChangeDrag(float seconds, float dragAmount)
    {
        accelerationTimeAirborne = dragAmount;
        accelerationTimeGrounded = dragAmount;
        yield return new WaitForSeconds(seconds);
        accelerationTimeAirborne = baseAccelerationTimeAirborne;
        accelerationTimeGrounded = baseAccelerationTimeGrounded;
    }

    private IEnumerator SetInputScale(float seconds, float scale)
    {
        inputScale = scale;
        yield return new WaitForSeconds(seconds);
        inputScale = 1f;
    }

    private IEnumerator DashCooldown(){
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
    }
}

