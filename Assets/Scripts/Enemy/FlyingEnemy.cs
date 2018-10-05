using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour {
    public float speed;
    public int enemyHealth;
    [SerializeField]
    private float frequency = 20f;
    [SerializeField]
    private float attackDistance;
    [SerializeField]
    private float magnitude = 0.5f;

    private bool facingRight = true;
    [SerializeField]
    private GameObject player;
    Vector3 pos, localScale;
    // Use this for initialization
    void Start () {
        pos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        localScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (System.Math.Abs(Vector2.Distance(transform.position, player.transform.position)) < attackDistance)
        {
            Attack();
        }
        
    }

    

    void Attack()
    {
       transform.position = Vector2.MoveTowards(pos, player.transform.position, speed * Time.deltaTime);
    }
}
