using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {

    public int thisId;
    public string thisSceneName;

    public int goToID;
    public string otherSceneName;

    public GlobalScene GManager;

    void Awake() {

    }

    void Start () {
        GManager = GlobalScene.instance;
        
    }

    void Update() {

    }
	void OnTriggerEnter2D(Collider2D collider) {
        // TODO: Get LevelTransitionHandler pair, move to different scene in that position
        if (collider.tag == "Player")
        {
            Debug.Log("fired");
            LevelTransitionHandler.instance.LoadNewScene(goToID, otherSceneName);
        }
    }
}   
