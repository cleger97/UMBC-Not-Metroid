using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Augment : MonoBehaviour {
    public string Name;
    public int augmentID;
    public string description;
    public Sprite image;
    // augment delegate function
    // when we make a new augment we assign the delegate a function
    // should be a function with no parameters - should have all data needed
    // internally.
    public UnityEvent onLoad;
    public void initializeAugment(string name, int id, string desc) {
        this.name = name;
        this.augmentID = id;
        this.description = desc;
    }

    public void LoadAugment() {
        onLoad.Invoke();
    }

}
