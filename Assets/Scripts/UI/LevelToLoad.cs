﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelToLoad : MonoBehaviour {

    public int sceneIndex;

private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision");
        if(col.tag == "Player")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
