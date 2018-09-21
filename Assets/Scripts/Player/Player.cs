using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static public Player instance = null;

    PlayerWeapon weapon;
    BoxCollider2D boxColl;
    Rigidbody2D rb2D;
    PlayerAugments augmentList;
    // external variable
    // will be changed by collisions with walls
    public bool isOnWall = false;

    float moveSpeed = 3f;
    float jump = 5f;

    int horizontalDirection = 0;

    float movementLockOut = 0.0f;

    float wallSlideSpeed = 1f;
    bool doubleJumped = false;

    bool isGrounded;

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
        if (movementLockOut > 0) {
            movementLockOut -= Time.deltaTime;
            return;
        }

        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");

        float boxCheckX = transform.position.x;
        float boxCheckY = transform.position.y - (boxColl.size.y / 2);
        groundCheckPosition = new Vector2(boxCheckX, boxCheckY);

        bool isDash = false;

        bool isJump = HandleJumpInput(out isDash);

        // -1 for left, 0 for not moving, 1 for right
        int direction = (horiz != 0) ? (int)Mathf.Sign(horiz) : 0;
        horizontalDirection = direction;
        // calculate position of check
        


        float vertModifier = (isJump) ? jump : 0;

        float vSpeed = (vertModifier > 0) ? vertModifier : rb2D.velocity.y;
        
        if (isOnWall && !isJump) {
            vSpeed = -wallSlideSpeed;
        } else if (isOnWall && isJump) {
            direction = -1 * direction;
            movementLockOut = 0.4f;
        }
        
        
        Vector2 velocity = new Vector2(moveSpeed * direction, vSpeed);

        Debug.Log("velocity = " + velocity.y);
        rb2D.velocity = velocity;
	}

    public bool HandleJumpInput(out bool isDash) {
        bool jumpInput = Input.GetButtonDown("Jump");
        isDash = Input.GetButtonDown("Dash");
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
