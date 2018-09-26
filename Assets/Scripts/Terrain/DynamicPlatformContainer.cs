using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicPlatformContainer : MonoBehaviour {
	public int maxPlatforms = 2;
	// Update is called once per frame
	public void ValidateCount () {
		// simple check to make sure we only have a certain amount of platforms
		if (transform.childCount > maxPlatforms) {
			for (int i = 0; i < transform.childCount - maxPlatforms; i++) {
				Transform t = transform.GetChild(0);
				t.parent = null;
				Destroy(t.gameObject);
			}
		}
	}
}
