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
		GameObject[] cameraGOARR = GameObject.FindGameObjectsWithTag("Virtual Camera");

        GameObject cameraGO = null;
        foreach (GameObject cameraGOA in cameraGOARR) {
            if (cameraGOA.scene == scene) {
                cameraGO = cameraGOA;
            }
        }

        if (cameraGO == null) { return;} 

		CinemachineVirtualCamera camera2d = cameraGO.GetComponent<CinemachineVirtualCamera>();

		camera2d.m_Lens.OrthographicSize = 5f;

		GameObject p = Player.instance.gameObject;
		camera2d.Follow = p.transform;
	}
	

}
