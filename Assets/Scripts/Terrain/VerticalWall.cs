using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWall : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collision Entered");
        Collider2D otherColl = collision.collider;
        if (otherColl.tag.Equals("Player")) {
            otherColl.GetComponent<Player>().isOnWall = true;
        }
    }

    void OnCollisionExit2D (Collision2D collision) {
        Collider2D otherColl = collision.collider;
        if (otherColl.tag.Equals("Player")) {
            otherColl.GetComponent<Player>().isOnWall = false;
        }
    }
}


