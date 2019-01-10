using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour {

	public float projectileTime = 1f;

	void Awake() {
		AudioSource beamSound = gameObject.GetComponent<AudioSource>();

		// better safe than NullPointerException
		if (beamSound != null) {
			beamSound.volume = AudioVolumeController.SFXVolPercent;
		}
		
	}

	public void SetDespawnTimer(float time) {
		projectileTime = time;
		StartCoroutine(StartFire(this.gameObject));
	}

	IEnumerator StartFire(GameObject beam) {
		yield return new WaitForSeconds(projectileTime);

		Destroy(beam);

		yield break;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Wall" || collider.tag == "Enemy") {
			Destroy(this.gameObject);
		}
	}
	
}
