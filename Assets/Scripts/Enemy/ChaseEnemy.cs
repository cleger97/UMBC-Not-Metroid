using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour {
    public bool isPatrol = true;
    public int enemyHealth;
    public float activeDistance;
    public float speed;
    public float knockBackSpeed;
    public ParticleSystem ps;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    float chaseDistance = 10f;
    private bool movingRight = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public Transform groundDetection;
    [SerializeField]
    private GameObject healthBar;
    private Vector3 localScale;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        localScale = healthBar.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        localScale.x = enemyHealth * .05f;
        healthBar.transform.localScale = localScale;
        if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < activeDistance)
        {
            isPatrol = true;
        }
        else if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) > activeDistance)
        {
            isPatrol = false;
        }
        
        if (isPatrol)
        {
            //transform.Translate(Vector2.right * speed * Time.deltaTime);

            RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);

            if (groundInfo.collider == false)
            {
                if (movingRight)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                    movingRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    movingRight = true;
                }
            }
            if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < chaseDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {

            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
        if (other.tag == "Weapon")
        {
            enemyHealth--;
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(-knockBackSpeed, 1.7f);
            }
            else
            {
                rb.velocity = new Vector2(knockBackSpeed, 1.7f);
            }
            Instantiate(ps, transform.position, Quaternion.identity);
            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        if (other.tag == "Player")
        {

            Attack();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            if (transform.position.x < player.transform.position.x)
            {
                rb.velocity = new Vector2(-knockBackSpeed, 1.7f);
            }
            else
            {
                rb.velocity = new Vector2(knockBackSpeed, 1.7f);
            }
            
        }
    }
    private void Attack()
    {
        Debug.Log("Attacking!");
    }
}
