using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperEnemy : MonoBehaviour {
    public bool canShoot = false;
    public float shootingDistance;
    public int enemyHealth;
    public float shotReset = 3f;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject _laserPrefab;
    public Transform groundDetection;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
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
