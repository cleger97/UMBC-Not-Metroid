using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyV2 : Enemy {
    private bool isPatrol = true;
    private bool facingRight = true;
    //private Rigidbody2D rb;
    // Use this for initialization
    public override void Start () {
        base.Start();
        //rb = GameObject.FindObjectOfType<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 HBpos = new Vector3(transform.position.x, transform.position.y + .8f, transform.position.z);
        HealthBar.value = enemyHealth;
        HealthBar.transform.position = HBpos;
        Flip();
        if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) > activeDistance)
        {
            isPatrol = false;
        }
        else if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < activeDistance)
        {
            isPatrol = true;
        }
        if (isPatrol)
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

    }
    public void Flip()
    {
        Debug.Log("in flip");
        if ((transform.position.x < player.transform.position.x))
        {
            if (facingRight)
            {
                sp.flipX = true;
            }
        }
        if ((transform.position.x > player.transform.position.x))
        {
            if (facingRight)
            {
                sp.flipX = false;
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
            int damageAmount = 1;
            TakeDamage(damageAmount);
            /* enemyHealth--;
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
             }*/
        }
        if (other.tag == "Player")
        {
            Attack();
        }
    }
}

