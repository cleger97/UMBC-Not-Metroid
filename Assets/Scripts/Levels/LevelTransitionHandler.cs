using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour {
    public static LevelTransitionHandler instance;
    // scene+id pair for entrance and exit

    public static Scene lastScene;

    private static int idOnLoad = -1;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        } else {
            Destroy(this.gameObject);
        } 
        SceneManager.sceneLoaded += OnSceneLoad;

        lastScene = SceneManager.GetActiveScene();
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        
        Transform player = Player.instance.transform;

        SceneManager.SetActiveScene(scene);

        GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

        foreach(GameObject door in doors) {
            LevelExit doorScript = door.GetComponent<LevelExit>();
            if (doorScript == null) continue;
            if (doorScript.thisId == LevelTransitionHandler.idOnLoad) {
                player.position = door.transform.GetChild(0).position;
            }
        }

        if (lastScene != SceneManager.GetActiveScene()) {
            SceneManager.UnloadSceneAsync(lastScene);
        }
        idOnLoad = -1; // don't move objects that load in other ways
    }

    public void LoadNewScene(int id, string scene) {
        Debug.Log("firing");
        idOnLoad = id;

        lastScene = SceneManager.GetActiveScene();

        //var allObjects = GameObject.FindObjectsOfType(typeof(Transform)) as Transform[];

        //foreach (Transform t in allObjects)
        //{
        //    GameObject.Destroy(t.gameObject);
        //}
        
        //Application.LoadLevelAdditive(Application.loadedLevel);


        SceneManager.LoadScene(scene, LoadSceneMode.Additive);                

    }
    

}