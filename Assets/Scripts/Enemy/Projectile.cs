using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float _speed;
    [SerializeField]
    private GameObject player;
    private Vector3 projectilePath;
    private Vector3 reflectPath;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        projectilePath = player.transform.position;
        reflectPath = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, projectilePath, _speed * Time.deltaTime);
        if(transform.position == projectilePath)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag == "Player")
            //Destroy(this.gameObject);
        if(other.tag == "Player")
        {
            transform.position = Vector2.MoveTowards(transform.position, reflectPath, _speed * Time.deltaTime);
        }
    }
}
