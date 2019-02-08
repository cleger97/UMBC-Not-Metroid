using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossDamage : MonoBehaviour {
    public int health;
    public ParticleSystem ps1, ps2;
    [SerializeField]
    private GameObject boss;
    // Use this for initialization
    void Start () {
        health = GameObject.FindObjectOfType<SpiderBoss>().health;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 pos = new Vector2(col.transform.position.x, col.transform.position.y);
        if (col.tag == "Weapon" && col.GetComponent<BoxCollider2D>().enabled)
        {
            
            health--;
            Instantiate(ps1, pos, Quaternion.identity);

        }
        
        if (health <= 0)
        {
            Instantiate(ps2, pos, Quaternion.identity);
            //Destroy(this.gameObject);
            Destroy(boss);
        }
    }
}
