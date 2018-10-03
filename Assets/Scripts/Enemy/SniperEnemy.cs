using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour {
    public bool canShoot = false;
    public float shootingDistance;
    public int enemyHealth;
    public float knockBackSpeed;
    public float shotReset = 3f;
    public ParticleSystem ps;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _laserPrefab;
    public Transform groundDetection;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        //rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        
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
            Instantiate(ps, transform.position, Quaternion.identity);
            if (enemyHealth <= 0)
            {
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
