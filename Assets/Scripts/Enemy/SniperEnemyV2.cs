using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemyV2 : Enemy {
    private bool facingRight = true;
    [SerializeField]
    private GameObject _laserPrefab;
    //private Rigidbody2D rb;
    public bool canShoot = false;
    public float shootingDistance;
    public float shotReset = 3f;
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

        if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < shootingDistance)
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Weapon")
        {
            int damageAmount = 1;
            TakeDamage(damageAmount);
            /*enemyHealth--;
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
    }
}
