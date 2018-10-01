using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour {
    public static LevelTransitionHandler instance;
    // scene+id pair for entrance and exit

    private int idOnLoad = 0;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        } else {
            Destroy(this.gameObject);
        } 
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        Transform player = Player.instance.transform;
    }

    void LoadNewScene(int id, string scene) {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
    

}