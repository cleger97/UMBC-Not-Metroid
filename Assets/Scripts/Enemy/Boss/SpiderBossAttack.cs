using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBossAttack : MonoBehaviour
{
    public int damageAmount = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Vector2 pos = new Vector2(col.transform.position.x, col.transform.position.y);
        if (col.tag == "Player")
        {
            Debug.Log("SpiderBoss attacking Player");
            col.GetComponent<PlayerHP>().currentHP -= damageAmount; ;
        }
    }
    }
