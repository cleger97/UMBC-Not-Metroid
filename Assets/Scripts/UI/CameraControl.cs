using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


public class CameraControl : MonoBehaviour {
	/*
		This class should be packaged with the Global Scene handler, and it doesn't need to be called
		So there is no point in making instances of it, nor is there a point of having an auxillary load
		The player (or objects) should be loading the Global Scripts as soon as it's necessary anyhow
	 */
	public void Awake() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		GameObject cameraGO = GameObject.FindGameObjectsWithTag("Virtual Camera")[0];
		CinemachineVirtualCamera camera2d = cameraGO.GetComponent<CinemachineVirtualCamera>();

		GameObject p = GameObject.Find("Player");
		camera2d.Follow = p.transform;
	}
	

}
