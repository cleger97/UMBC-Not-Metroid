using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed;
    //public float dashSpeed;
    //public float dashDistance;
    public float activeDistance;
    //public float stoppingDistance;
   // public float retreatDistance;
    //public float shootingDistance;
    public int enemyHealth;
    public SpriteRenderer sp;
    public float knockBackSpeed;
    public ParticleSystem ps1, ps2;
    public Rigidbody2D rb;
    //public bool dash = false;

    //public bool patrol = true;
    //public bool sniper = false;
    //public bool copyCat = false;
    //public bool canShoot = false;
    //public float shotReset = 3f;
    //private float canDash = 3f;
    //loat reset = 3f;
    public bool movingRight = true;
    
    public GameObject player;
    //[SerializeField]
    //private GameObject _laserPrefab;
    
    public Slider HealthBar;
    public Transform groundDetection;

    // Use this for initialization
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GameObject.FindObjectOfType<Rigidbody2D>();
        HealthBar.transform.position = transform.position;
        HealthBar.maxValue = enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    public void TakeDamage(int damageAmount)
    {
        Debug.Log("in takedamage");
        enemyHealth -= damageAmount;
        if (transform.position.x < player.transform.position.x)
        {
            rb.velocity = new Vector2(-knockBackSpeed, 1.7f);
        }
        else
        {
            rb.velocity = new Vector2(knockBackSpeed, 1.7f);
        }
        Instantiate(ps1, transform.position, Quaternion.identity);
        if (enemyHealth <= 0)
        {
            Instantiate(ps2, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
   /* public void OnTriggerEnter2D(Collider2D other)
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
        if(other.tag == "Weapon")
        {
            int damageAmount = 1;
            TakeDamage(damageAmount);
        }
        if(other.tag == "Player")
        {
            Attack();
        }
    }*/
    /*private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            enemyHealth--;
            if (enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }*/
    public void Attack()
    {
        Debug.Log("Attacking!");
    }
}


