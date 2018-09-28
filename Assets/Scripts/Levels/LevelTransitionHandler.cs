using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionHandler : MonoBehaviour {
    public GameObject[] entryList;
    public GameObject[] exitList;

    public static LevelTransitionHandler instance;

    void Awake() {
        if (instance == null) {
            DontDestroyOnLoad(this);
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

}
