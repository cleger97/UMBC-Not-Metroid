using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public ParticleSystem ps;
    public int damageAmount = 10;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject player;
    private GameObject enemy;
    private Vector3 projectilePath;
    private Vector3 reflectPath;
    private Vector3 pos;
    // Use this for initialization
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Sniper");
        player = GameObject.FindGameObjectWithTag("Player");
        projectilePath = player.transform.position;
        reflectPath = this.transform.position;
       // ps = GameObject.FindObjectOfType<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, projectilePath, _speed * Time.deltaTime);
        if(transform.position == projectilePath)
        {
            pos = transform.position;
            Instantiate(ps, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            pos = transform.position;
            player.GetComponent<PlayerHP>().currentHP -= damageAmount;
            Instantiate(ps, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(other.tag == "Weapon")
        {
            Debug.Log("Reflect");
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, _speed * Time.deltaTime);
        }
    }
}
