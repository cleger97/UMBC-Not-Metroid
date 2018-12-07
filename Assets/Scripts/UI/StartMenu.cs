using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	public Button startButton;

	void Start() {
		if (startButton == null) {
			startButton = gameObject.GetComponent<Button>();
		}
		startButton.onClick.AddListener(LoadGame);
	}

	void LoadGame() {
		Debug.Log("Loading Game...");
		LevelTransitionHandler.instance.LoadNewScene(0,"First Tiled Level");
	}

	void Update() {
		if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Menu")) {
			LoadGame();
		}
	}
}
