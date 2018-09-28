using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseEnemy : MonoBehaviour {
    public bool isPatrol = true;
    public int enemyHealth;
    public float activeDistance;
    public float speed;
    [SerializeField]
    float chaseDistance = 10f;
    private bool movingRight = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    public Transform groundDetection;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
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
        if (other.tag == "Player")
        {
            Attack();
        }
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
    private void Attack()
    {
        Debug.Log("Attacking!");
    }
}
