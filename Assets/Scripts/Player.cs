using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    BoxCollider2D boxColl;
    Rigidbody2D rb2D;

    float moveSpeed = 3f;
    float accel = 2f;

	// Use this for initialization
	void Start () {
        boxColl = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float vert = Input.GetAxisRaw("Vertical");
        float horiz = Input.GetAxisRaw("Horizontal");

        // -1 for left, 0 for not moving, 1 for right
        int direction = (horiz != 0) ? (int)Mathf.Sign(horiz) : 0;
        
        Vector2 velocity = new Vector2(moveSpeed * direction, rb2D.velocity.y);
        rb2D.velocity = velocity;
	}
}
