using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed;
    
    public float activeDistance;
    
    public int enemyHealth;
    public SpriteRenderer sp;
    public float knockBackSpeed;
    public ParticleSystem ps1, ps2;
    public Rigidbody2D rb;
    
    public bool movingRight = true;
    public GameObject player;
    public Slider HealthBar;
    public Transform groundDetection;

    // Use this for initialization
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
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
            Destroy(gameObject);
        }
    }
    
    public void Attack()
    {
        Debug.Log("Attacking!");
    }
}


