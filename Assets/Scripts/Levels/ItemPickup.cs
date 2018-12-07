using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemPickup : MonoBehaviour {

	public GameObject itemInWorld;
	public AudioClip ItemPickupSFX;
	public AudioSource audio;
	public float newJumpHeight;

	public ActionScript doWhenFinished;

	public GameObject itemCanvas;
	// Use this for initialization
	private Player inst;
	void Start () {
		inst = Player.instance;
		audio = GetComponent<AudioSource>();

		if (audio != null) {
			audio.clip = ItemPickupSFX;
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			audio.Play();

			Debug.Log("item picked up");
			Augment();
			Music.inst.Toggle();

			// Pause the game.
			Time.timeScale = 0f;
			
			itemCanvas.GetComponent<ItemCanvas>().StartText("High Jump", "Increased Jump Height");
			StartCoroutine(WaitForAudio());
		}
	}

	

	void Augment() {
		// High Jump
		inst.jumpHeight = newJumpHeight;
		inst.UpdateJumpHeight(newJumpHeight);
	}

	IEnumerator WaitForAudio() {
		yield return new WaitUntil(() => audio.isPlaying == false);
		Finish();
	}

	void Finish() {
		itemInWorld.SetActive(false);
		Time.timeScale = 1f;
		Music.inst.Toggle();

		doWhenFinished.Action();
	}
}
