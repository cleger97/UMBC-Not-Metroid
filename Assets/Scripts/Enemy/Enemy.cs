using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

    public float speed;
    public float activeDistance;
    public float stoppingDistance;
    public float retreatDistance;
    public float shootingDistance;
    public int enemyHealth;
    public bool patrol = true;
    public bool sniper = false;
    public bool copyCat = false;
    public bool canShoot = false;
    public float shotReset = 3f;

    private bool movingRight = true;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _laserPrefab;
    public Transform groundDetection;
    
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < activeDistance)
        {
            patrol = false;
        }
        else if (Vector2.Distance(transform.position, player.transform.position) > activeDistance)
        {
            patrol = true;
        }

        if (patrol)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

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
        }
        if (!patrol)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < 0)
            {
                movingRight = false;
            }

            if (Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < stoppingDistance && Vector2.Distance(transform.position, player.transform.position) > stoppingDistance)
            {

                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.transform.position) < retreatDistance)
            {

                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, -speed * Time.deltaTime);
            }

        }
        if (sniper)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < shootingDistance)
            {
                if (canShoot)
                {
                    Instantiate(_laserPrefab, transform.position, Quaternion.identity);
                    canShoot = false;

                }
                shotReset -= Time.deltaTime;
                if (shotReset <= 0)
                {
                    shotReset = 2f;
                    canShoot = true;
                }
            }
        }
        if (copyCat)
        {
           

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
        if(other.tag == "Player")
        {
            Attack();
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
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


