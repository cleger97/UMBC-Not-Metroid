using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    static public Player instance = null;

    PlayerWeapon weapon;
    BoxCollider2D boxColl;
    Rigidbody2D rb2D;
    PlayerAugments augmentList;

    float moveSpeed = 3f;
    float jump = 5f;

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
    }
    void Start () {
        boxColl = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        weapon = GetComponent<PlayerWeapon>();
        groundMask = LayerMask.GetMask("Ground");
        groundCheckPosition = Vector2.zero;

        isGrounded = false;
	}
	
	// Update is called once per frame
	void Update () {
        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");
        bool isJump = Input.GetButtonDown("Jump");

        // -1 for left, 0 for not moving, 1 for right
        int direction = (horiz != 0) ? (int)Mathf.Sign(horiz) : 0;

        // calculate position of check
        float boxCheckX = transform.position.x;
        float boxCheckY = transform.position.y - (boxColl.size.y / 2);
        groundCheckPosition = new Vector2(boxCheckX, boxCheckY);
               
        isGrounded = (checkCollide(groundCheckPosition, 0.5f, groundMask)) ? true : false;     
        float vertModifier = (isJump && isGrounded) ? jump : 0;
        
        
        Vector2 velocity = new Vector2(moveSpeed * direction, rb2D.velocity.y + vertModifier);
        rb2D.velocity = velocity;
	}

    public void HandleFireInput() {
        bool isPrimaryFire = Input.GetButtonDown("PrimaryFire");
        //bool isSecondaryFire = Input.GetButtonDown("SecondaryFire");
        //bool isSwitch = Input.GetButtonDown("SwitchWeapon");


    }

    // Functionality - What the player can do
    public void Activate(int id) {
        weapon.Activate(id);
    }

    // Tools to use in debugging/getting information
    public static bool checkCollide(Vector2 position, float radius, LayerMask layer) {
        return Physics2D.OverlapCircle(position, radius, layer);
    }

    void OnDrawGizmos() {
        Gizmos.color = (isGrounded) ? Color.green : Color.red;
		Gizmos.DrawWireSphere (groundCheckPosition, 0.5f);
    }

}
