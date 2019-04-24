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
    [HideInInspector]public float jumpVelocity;
    [HideInInspector]public Vector3 velocity;
    private float velocityXSmoothing;
    private bool facingRight = true;
    public bool doubleJump = false;
    [HideInInspector]public Controller2D controller;
    private Animator animator;
    private FloatTimer inputScale;
    private FloatTimer accelerationTimeAirborne;
    private FloatTimer accelerationTimeGrounded;
    private BoolTimer dashOnCooldown;
    private BoxCollider2D collider;
    private int platforms = 1;
    private PlayerWeapon weapon;

    private float groundingDelay = 0f;
    private float maxGroundingDelay = 0.15f;

    public float hitstunMAX = 0.4f;
    public float currentHitstun = 0f;
    public bool isHitstun = false;

    public float hitstunKBSmooth = 0.8f;

    public float kbVel = 5f;
    public float yKBVel = 3f;


    //public bool isEnabled = true;

    private void OnEnable() {
        if (instance == null || instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }


    private void Awake()
    {
        weapon = FindObjectOfType<PlayerWeapon>();

        if (instance == null || instance == this)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }

        if (instance != null) {
            Debug.Log(instance);
            Debug.Log(instance.gameObject);

            Debug.Log("Instances same: " + (instance == this));
        } else {
            Debug.Log("instance = null");
        }

    /*  if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    */

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

    private void Start()
    {
        if (audio != null)
        {
            if (AudioVolumeController.inst != null)
            {
                Debug.Log("Added volume pulse");
                audio.volume = AudioVolumeController.SFXVolPercent;
                AudioVolumeController.inst.RegisterAudio(AudioType.SFX, this.gameObject);

            } else
            {
                audio.volume = 1;
            }

        } else
        {
            Debug.LogWarning("Audio not initalized");
        }

    }

    public void UpdateJumpHeight(float jumpHeight) {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
    }

    public float GetWeaponDamage() {
        if (weapon == null) {
            return 0;
        }
        Weapon currentWeapon = weapon.getCurrentWeapon();

        if (currentWeapon == null) {
            return 0;
        }

        return currentWeapon.damage;
    }

    void Update()
    {
        if (MenuHandle.instance != null && MenuHandle.instance.isPaused()) {
          return;
        }

        // Animator data
        bool isJumping = false;
        bool isAttacking = false;

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
        else
        {
            if (controller.collisions.lastJumpState)
            {
                groundingDelay = maxGroundingDelay;
            }
            else
            {
                if (groundingDelay > 0)
                {
                    groundingDelay -= Time.deltaTime;
                    if (groundingDelay < 0) { groundingDelay = 0; }
                }
            }
        }

        //if (isHitstun) {

        //    currentHitstun -= Time.deltaTime;
        //    if (currentHitstun <= 0) {
        //        isHitstun = false;
        //        currentHitstun = 0;
        //    }

        //} else {

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
            if (Input.GetButtonDown("Jump"))
            {
                if (controller.collisions.below || groundingDelay > 0) // Regular jump
                {
                    audio.clip = clips[0];
                    audio.Play();
                    velocity.y = jumpVelocity;

                    groundingDelay = 0;
                    isJumping = true;
                }
                else if (controller.collisions.left)
                { // Left wall jump
                    audio.clip = clips[0];
                    audio.Play();
                    velocity.y = jumpVelocity;
                    velocity.x = jumpVelocity / 1.5f;
                    inputScale.UpdateValue(.1f, 0);
                }
                else if (controller.collisions.right)
                { // Right wall jump
                    audio.clip = clips[0];
                    audio.Play();
                    velocity.y = jumpVelocity;
                    velocity.x = -jumpVelocity / 1.5f;
                    inputScale.UpdateValue(.1f, 0);
                }
                else if (!doubleJump)
                {    // Double jump
                    audio.clip = clips[0];
                    audio.Play();
                    velocity.y = jumpVelocity;
                    doubleJump = true;
                    isJumping = true;
                }
            }
            // Ignore platform if pressing down
            controller.ignorePlatform = input.y < 0;

            // Add gravity
            velocity.y += gravity * Time.deltaTime;

            weapon.UpdateState();
            isAttacking = weapon.isAttacking;

        //}

        // Handle animator motions
        HandleAnimator(isJumping, isAttacking);

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

        
    }

    public void DamageKnockback() {

        Vector3 negativeVelocity = new Vector3(Mathf.Sign(velocity.x) * -1 * kbVel, yKBVel, velocity.z);

        this.velocity = negativeVelocity * hitstunKBSmooth;

        isHitstun = true;
        currentHitstun = hitstunMAX;

    }

    public void ProjectileSpawn()
    {
        LaserBurst laser = FindObjectOfType<LaserBurst>();
        laser.ProjectileSpawn();
    }

    public void HandleAnimator(bool isJumping, bool isAttacking) {
        // Reset triggers
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Jump");

        //Debug.Log(velocity.x);

        Vector2 input = new Vector2( Input.GetAxisRaw("Horizontal") , Input.GetAxisRaw("Vertical") );

        if (input.x == 0) {
            animator.SetBool("isWalking", false);
        }

        // Handle attacking
        if (isAttacking)
        {
            if(weapon.currentWeapon == 1)
            {
                animator.SetTrigger("Attack");
            }

            if (weapon.currentWeapon == 0)
            {
                animator.SetTrigger("BeamAttack");
            }
        }

        else if (isJumping) {
            animator.SetTrigger("Jump");
        } 

        else if (Mathf.Abs(input.x) > 0) {
            animator.SetBool("isWalking", true);
        }

        else {
            // default
            return;
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

