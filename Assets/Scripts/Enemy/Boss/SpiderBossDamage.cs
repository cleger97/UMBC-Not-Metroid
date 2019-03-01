using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossDamage : MonoBehaviour {
    public int health;
    public ParticleSystem ps1, ps2;
    private float damageTime;
    [SerializeField]
    private GameObject boss;

    [SerializeField] private SpriteRenderer sr;
    // Use this for initialization
    void Start () {
        health = GameObject.FindObjectOfType<SpiderBoss>().health;
	}
	
	// Update is called once per frame
	void Update () {
        damageTime -= Time.deltaTime;
        if (damageTime < 0)
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 pos = new Vector2(col.transform.position.x, col.transform.position.y);
        if (col.tag == "Weapon" && col.GetComponent<BoxCollider2D>().enabled)
        {
            damageTime = .1f;
            health--;
            Instantiate(ps1, pos, Quaternion.identity);
            sr.color = Color.black;
        }
        
        if (health <= 0)
        {
            Instantiate(ps2, pos, Quaternion.identity);
            //Destroy(this.gameObject);
            Destroy(boss);
        }
    }
}
