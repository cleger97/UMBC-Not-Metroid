using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SniperEnemy : MonoBehaviour {
    public bool canShoot = false;
    public float shootingDistance;
    public int enemyHealth;
    public float knockBackSpeed;
    public float shotReset = 3f;
    public ParticleSystem ps1, ps2;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _laserPrefab;
    public Transform groundDetection;
    [SerializeField]
    private SpriteRenderer sp;
    private bool facingRight = true;
    [SerializeField]
    private Slider HealthBar;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
       
        HealthBar.transform.position = transform.position;

        HealthBar.value = enemyHealth;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 HBpos = new Vector3(transform.position.x, transform.position.y + .87f, transform.position.z);
        HealthBar.value = enemyHealth;
        HealthBar.transform.position = HBpos;
        Flip();
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
    private void Flip()
    {
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
            enemyHealth--;
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
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
          
        }
    }
}
