using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpiderBoss : MonoBehaviour {
    public int health;
    public float speed;
    public int damage;
    public GameObject player;
    public float attackSpeed;
    public float timeBetweenAttacks;
    public float attackDuration;
    public ParticleSystem ps1, ps2;
    public SpriteRenderer sp;

    private float attackDuration2;
    private float attackTime;
    private bool facingRight = true;
    private Animator anim;
    private Slider healthBar;
    private int stages;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        healthBar = FindObjectOfType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        stages = 1;
        anim.SetBool("isWalking", false);
    }
	
	// Update is called once per frame
	void Update () {
        if (player != null)
        {
            if (stages == 1)
            {
                //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
                //anim.SetBool("isWalking", true);
                if (Time.time >= attackTime)
                {
                    //StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
	}
    IEnumerator Attack()
    {
        //player.GetComponent<Player>().TakeDamage(damage);
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.transform.position;

        float percent = 0;
        attackDuration2 = attackDuration;
        while (attackDuration2 > 0)
        {
            attackDuration2 -= Time.deltaTime;
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            //transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            transform.position = Vector2.MoveTowards(originalPosition, targetPosition, attackSpeed*Time.deltaTime);
            //yield return null;
            yield return new WaitForSeconds(attackDuration);
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
}
