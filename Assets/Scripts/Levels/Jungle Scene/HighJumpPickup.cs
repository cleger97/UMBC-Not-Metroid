using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpPickup : ActionScript {

	public GameObject doorToActivate;
	// Use this for initialization
	public override void Action() {
		doorToActivate.GetComponent<LevelExit>().isEnabled = true;

		doorToActivate.GetComponent<SpriteRenderer>().enabled = true;

		doorToActivate.GetComponent<BoxCollider2D>().enabled = true;

        gameObject.GetComponent<HighJump>().Action();
	}

	public void Start() {}
}
