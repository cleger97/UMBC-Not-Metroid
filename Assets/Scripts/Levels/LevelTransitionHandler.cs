using System.Collections;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransitionHandler : MonoBehaviour {
    public static LevelTransitionHandler instance;
    // scene+id pair for entrance and exit

    public static Scene lastScene;

    public static int idOnLoad = -1;

    public static int lastLoad = -1;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        } 
        SceneManager.sceneLoaded += OnSceneLoad;

        lastScene = SceneManager.GetActiveScene();
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode) {
        
        lastLoad = idOnLoad;
        SceneManager.SetActiveScene(scene);
       
        if (lastScene != SceneManager.GetActiveScene()) {
            StartCoroutine(UnloadScene());
        }
    }

    // unload scene coroutine
    // first -> unload the scene completely
    // second -> wait for scene to finish unload
    // third -> adjust player position accordingly
    // fourth -> return.
    IEnumerator UnloadScene() {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(lastScene); 
        yield return unload;
        FinishLoad();
        yield break;
    }

    private void FinishLoad() {
        Transform player = Player.instance.transform;
         GameObject[] doors = GameObject.FindGameObjectsWithTag("Door");

         foreach(GameObject door in doors) {
            LevelExit doorScript = door.GetComponent<LevelExit>();
            if (doorScript == null) continue;
            if (doorScript.thisId == LevelTransitionHandler.idOnLoad) {
                player.position = door.transform.GetChild(0).position;
            }
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