
/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static public Player instance = null;
    public GameObject platform;
    public GameObject platformContainer;
    public float pOffsetDistance = 1.6f;

    PlayerWeapon weapon;
    BoxCollider2D boxColl;
    Rigidbody2D rb2D;
    PlayerAugments augmentList;
    // external variable
    // will be changed by collisions with walls
    public bool isOnWall = false;

    public float moveSpeed = 3f;
    public float jump = 5f;

    public float dashDistance = 5f;
    public float dashSpeed = 1f;

    private int horizontalDirection = 0;

    public float movementLockOut = 0.0f;

    public float wallSlideSpeed = 1f;
    private bool doubleJumped = false;

    public bool isGrounded;

    Vector2 groundCheckPosition;
    LayerMask groundMask;

    // Use this for initialization

    void Awake() {
        if (instance != null) {
            Destroy (this);
        } else {
            DontDestroyOnLoad(this);
        }

        boxColl = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        weapon = GetComponent<PlayerWeapon>();
    }
    void Start () {
        
        groundMask = LayerMask.GetMask("Ground");
        groundCheckPosition = Vector2.zero;

        isGrounded = false;
	}
	
	// Update is called once per frame
	void Update () {
        HandleMovement();

        HandlePlatform();

	}

    private void HandleMovement() {
         if (movementLockOut > 0) {
            movementLockOut -= Time.deltaTime;
            return;
        }

        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");

        float boxCheckX = transform.position.x;
        float boxCheckY = transform.position.y - (boxColl.size.y / 2);
        groundCheckPosition = new Vector2(boxCheckX, boxCheckY);

        bool isDash = HandleDashInput();

        bool isJump = HandleJumpInput();

        // -1 for left, 0 for not moving, 1 for right
        int direction = (horiz != 0) ? (int)Mathf.Sign(horiz) : 0;
        horizontalDirection = direction;
        // calculate position of check
        
        Vector2 velocity = Vector2.zero;
        
        if (isDash) {
            velocity = HandleDashMovement(horiz, vert);
            movementLockOut = dashSpeed;
        } else {
            float vertModifier = (isJump) ? jump : 0;

            float vSpeed = (vertModifier > 0) ? vertModifier : rb2D.velocity.y;
        
            if (isOnWall && !isJump) {
                vSpeed = -wallSlideSpeed;
            } else if (isOnWall && isJump) {
                direction = -1 * direction;
                movementLockOut = 0.4f;
            }
            velocity = new Vector2(moveSpeed * direction, vSpeed);
        }

        rb2D.velocity = velocity;


    }

    private bool HandleDashInput() {
        bool isDash = Input.GetButtonDown("Dash");

        // TODO: Mid-Air Dash Lock (i.e. you can't dash infinite times in air)

        return isDash;
    }

    private Vector2 HandleDashMovement(float horiz, float vert) {
        Vector2 endMovement = Vector2.zero;

        float speedOfDash = (dashDistance / dashSpeed);
        
        if (horiz == 0 && vert == 0) {
            //Debug.Log("Case 1: no direction");
            return Vector2.zero;
            
        }
        else if (horiz == 0 ^ vert == 0) { // exclusive or: if horizontal or vertical are 0, but not both and not neither.
            endMovement = (vert == 0) ? new Vector2( Mathf.Sign(horiz) * speedOfDash , 0f) : new Vector2(0f, Mathf.Sign(vert) * speedOfDash );
            //Debug.Log("Speed Of Dash: " + speedOfDash);
            //Debug.Log("Directions: " + Mathf.Sign(horiz) + " : " + Mathf.Sign(vert));
            //Debug.Log("Case 2: One Direction");
        } else { 
            float angle45 = (Mathf.Sqrt(2)) / 2f; // On a unit circle, the 45 degree angle to create a line of size 1 has both X and Y of sqrt(2)/2.
            endMovement = new Vector2(Mathf.Sign(horiz) * speedOfDash * angle45, Mathf.Sign(vert) * speedOfDash * angle45 );
            //Debug.Log("Case 3: Both Directions");
        }

        // dash velocity shouldn't drop all the aerial velocity
        endMovement.y += rb2D.velocity.y;

        return endMovement;
    }

    private bool HandleJumpInput() {
    
        bool jumpInput = Input.GetButtonDown("Jump");
        
        bool grounded = (checkCollide(groundCheckPosition, boxColl.size.x / 2, groundMask)) ? true : false;
        
        // isGrounded = airborne;
        Debug.Log("Airborne: " + !grounded);

        if (isOnWall && jumpInput) { return true; }

        // if grounded reset double jump
        if (grounded) { doubleJumped = false; }

        if (grounded && jumpInput) { return true; }

        else if (doubleJumped == false && jumpInput) { doubleJumped = true; return true; }

        return false;
    }

    public void HandlePlatform() {
        bool isPlatforming = Input.GetButtonDown("Platform");
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // calculate direction
        
        Vector3 offset = new Vector3(Mathf.Sign(direction.x) * pOffsetDistance, Mathf.Sign(direction.y) * pOffsetDistance, transform.position.z);

        if (isPlatforming) {
            if (direction.x == 0 && direction.y == 0) { return; }

            GameObject newPlatform = Instantiate(platform, transform.position + offset, Quaternion.identity, platformContainer.transform);
            platformContainer.GetComponent<DynamicPlatformContainer>().ValidateCount();
            
        }

    }
    public void HandleFireInput() {
        //bool isPrimaryFire = Input.GetButtonDown("PrimaryFire");
        //bool isSecondaryFire = Input.GetButtonDown("SecondaryFire");
        //bool isSwitch = Input.GetButtonDown("SwitchWeapon");
    }



    // Functionality - What the player can do
    public void Activate(int id) {
        weapon.Activate(id);
    }

    // Tools to use in debugging/getting information
    public static bool checkCollide(Vector2 position, float rad, LayerMask layer) {
        return Physics2D.OverlapCircle(position, rad , layer);
    }

    void OnDrawGizmos() {
        Gizmos.color = (isGrounded) ? Color.green : Color.red;
        float rad = (boxColl == null) ? 0.3f : (float)boxColl.size.x / 2;
		Gizmos.DrawWireSphere (groundCheckPosition, rad);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision Entered");
        Collider2D otherColl = collision.collider;
        float distance = (boxColl.size.x / 2) + 0.1f;
        if (otherColl.tag.Equals("Wall")) {
            if (horizontalDirection == 0) {return;} // will never be true if falling, and should never hit a wall otherwise
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * horizontalDirection, distance, LayerMask.GetMask("Ground"));  

            if (hit != false) {
                isOnWall = true;
                movementLockOut = 0f;
            }

        }

    }

    void OnCollisionExit2D(Collision2D collision) {
        Collider2D otherColl = collision.collider;
        float distance = (boxColl.size.x / 2) + 0.1f;
        if (otherColl.tag.Equals("Wall")) {
            if (horizontalDirection == 0) {return;} // will never be true if falling, and should never hit a wall otherwise
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * -1 * horizontalDirection, distance, LayerMask.GetMask("Ground"));  

            if (hit == false) {
                isOnWall = false;
            }

        }
    }


}
*/