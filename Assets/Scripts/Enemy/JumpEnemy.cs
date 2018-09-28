using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemy : MonoBehaviour {
    public float speed;
    public float jumpSpeed;
    public int enemyHealth;
    public float activeDistance = 10;
    private float jumpTimer;
    private float jumpResetTime = 2f;
    public bool canJump = true;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public Transform groundDetection;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();
        jumpTimer = jumpResetTime;
    }
	
	// Update is called once per frame
	void Update () {
        jumpTimer -= Time.deltaTime;
        if(jumpTimer < 0)
        {
            canJump = true;
            jumpTimer = jumpResetTime;
        }
        if (canJump)
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
            if (groundInfo.collider == true)
            {
                Jump();
                canJump = false;
            }
        }
        if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < activeDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime) ;
        }
    }
    void Jump()
    {
        rb.velocity = new Vector2(0, jumpSpeed );
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            enemyHealth--;
            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
