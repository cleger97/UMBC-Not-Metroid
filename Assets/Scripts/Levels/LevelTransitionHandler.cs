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

    private float timer = 0f;

    public float MAXTimer = 2f;

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

    void Update() {
        if (timer > 0f) {
            timer -= Time.deltaTime;
        } else if (timer < 0f) {
            timer = 0f;
        }
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
        if (lastScene != null) {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(lastScene); 
            yield return unload;
            FinishLoad();
            yield break;
        }
        
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
        player.gameObject.SetActive(true);
        idOnLoad = -1; // don't move objects that load in other ways
        timer = MAXTimer;
    }

    public void LoadNewScene(int id, string scene) {

        if (timer != 0f) {
            Debug.Log("Minimum time not elapsed");
            return;
        }
        
        Debug.Log("firing");
        idOnLoad = id;

        lastScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(scene, LoadSceneMode.Additive);                

    }

    public void ReturnToMain() {
        SceneManager.LoadScene("Start Menu");
        Player.instance.gameObject.SetActive(false);
    }
    

}