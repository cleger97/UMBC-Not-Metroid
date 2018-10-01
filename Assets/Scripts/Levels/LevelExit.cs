using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour {

    public int thisId;
    public string thisSceneName;

    public int goToID;
    public string otherSceneName;

    void Awake() {

    }

    void Start () {

    }

    void Update() {

    }
	void OnTriggerEnter2D(Collider2D collider) {
        // TODO: Get LevelTransitionHandler pair, move to different scene in that position
        Debug.Log("fired");
        LevelTransitionHandler.instance.LoadNewScene(goToID, otherSceneName);
    }
}   
