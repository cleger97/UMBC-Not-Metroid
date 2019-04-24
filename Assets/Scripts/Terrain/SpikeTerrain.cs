using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTerrain : MonoBehaviour {

    public float DamagePerSecond = 3f;

    Collider2D colliding;

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            colliding = collider;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.tag.Equals("Player")) {
            colliding = null;
        }
    }

    void Update() {
        if (colliding != null) {
            colliding.GetComponent<PlayerHP>().TakeDamage(DamagePerSecond * Time.deltaTime, false, false);
        }
    }

}
