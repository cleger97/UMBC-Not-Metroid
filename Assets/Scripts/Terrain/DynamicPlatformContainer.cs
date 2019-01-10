using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicPlatformContainer : MonoBehaviour {
	public int maxPlatforms = 2;
	// Code to make sure there exists exactly one DynamicPlatformContainer at any given time
	public static DynamicPlatformContainer instance {
		get {
			if (inst == null) {
				GameObject DPC = Instantiate(Resources.Load("Level_Prefabs/PlatformContainer")) as GameObject;
				inst = DPC.GetComponent<DynamicPlatformContainer>();
				DontDestroyOnLoad(inst);
				return inst;
			} else {
				return inst;
			}
		}

		set {
			inst = value;
		}
	}

	private static DynamicPlatformContainer inst = null;
	void Awake() {
		if (inst != null) {
			Destroy(this.gameObject);
			return;
		} else {
			DontDestroyOnLoad(this);
			inst = this;
		}
		

		SceneManager.sceneLoaded += OnSceneLoad;
	}

	void OnSceneLoad(Scene scene, LoadSceneMode mode) {
		while (transform.childCount > 0) {
			Transform t = transform.GetChild(0);
			t.parent = null;
			Destroy(t.gameObject);
		}
	}

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
