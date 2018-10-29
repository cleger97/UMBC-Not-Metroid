using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxButton : MonoBehaviour {
    [SerializeField]
    private GameObject block;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Weapon")
        {
            Instantiate(block, new Vector3(transform.position.x + 1.5f, transform.position.y + 8, transform.position.z), Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
