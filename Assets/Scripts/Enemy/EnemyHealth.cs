using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    private Vector3 localScale;
    private Vector3 multVec;
    // Use this for initialization
    void Start () {
        multVec = new Vector3(.1f, 0, 0);
        localScale = transform.localScale;

    }
	
	// Update is called once per frame
	void Update () {
        localScale.x = GameObject.FindObjectOfType<PatrolEnemy>().enemyHealth;
        transform.localScale = localScale * .05f;
    }
}
