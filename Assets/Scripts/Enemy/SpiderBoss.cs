using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpiderBoss : MonoBehaviour {
    public int health;
    public float speed;
    private float startSpeed;
    public int damage;
    public GameObject player;
    public float attackSpeed;
    public float timeBetweenAttacks;
    public float attackDuration;
    public ParticleSystem ps1, ps2;
    public SpriteRenderer sp;
    public float walkDistance;
    [SerializeField]
    private GameObject _laserPrefab;
    private int count = 0;
    private bool walkLeft;
    private float attackDuration2;
    private float attackTime;
    private Animator anim;
    private Slider healthBar;
    private int stages;
    private Vector3 originalPosition;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        walkLeft = true;
        stages = 1;
        anim.SetBool("isWalking", false);
        originalPosition = transform.position;
        startSpeed = speed;
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (stages == 1)
            {
                
                anim.SetBool("isWalking", true);
                if (count > 1)
                {
                    speed = 0;
                    stages = 2;
                    count = 0;
                }
                if (walkLeft && (anim.GetBool("isWalking") == true))
                {
                    anim.SetBool("Slam", false);
                    Vector3 newPosition = new Vector3(originalPosition.x - walkDistance, transform.position.y);
                    transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
                    
                    if(transform.position == newPosition)
                    {
                        walkLeft = false;
                        count++;
                    }
                }
                if (!walkLeft && (anim.GetBool("isWalking") == true))
                {
                    transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
                    if(transform.position.x >= originalPosition.x)
                    {
                        
                        walkLeft = true;
                        
                    }
                }
                
            }
            if(stages == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
                int rand = Random.Range(0, 100);
                if (rand >= 35 && rand < 75)
                {
                    anim.SetBool("newCharge", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("Slam", true);
                    stages = 1;
                }
                if(rand >= 75)
                {
                    anim.SetBool("Slam", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("newCharge", true);
                    stages = 1;
                }
                if(rand < 35)
                {
                    anim.SetBool("Shoot", true);
                    anim.SetBool("Slam", false);
                    anim.SetBool("isWalking", false);
                    anim.SetBool("newCharge", false);
                    stages = 1;
                }
            }
        }
	}
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 pos = new Vector2(col.transform.position.x, col.transform.position.y);
        if (col.tag == "Weapon")
        {
            
            health--;
            Instantiate(ps1, pos, Quaternion.identity);
            
        }
        if(health <= 0)
        {
            Instantiate(ps2, pos, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    private void ResetSpeed()
    {
        speed = startSpeed;
        anim.SetBool("Slam", false);
        anim.SetBool("newCharge3", false);
        anim.SetBool("Shoot", false);
        anim.SetBool("isWalking", true);
    }
    private void newCharge2()
    {
        anim.SetBool("newCharge", false);
        anim.SetBool("newCharge2", true);
    }
    private void newCharge3()
    {
        anim.SetBool("newCharge2", false);
        anim.SetBool("newCharge3", true);
    }
    private void Shoot()
    {
        Instantiate(_laserPrefab, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
    }
}
