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
		LevelTransitionHandler.instance.LoadNewScene(-1,"Second Leve");
	}

	void Update() {
		if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Menu")) {
			LoadGame();
		}
	}
}
