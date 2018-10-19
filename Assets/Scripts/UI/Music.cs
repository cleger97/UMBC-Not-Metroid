using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public static Music inst = null;

    
	// Use this for initialization
	void Start () {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (inst == null) {
            DontDestroyOnLoad(this);
            inst = this;
        } else {
            audio.Stop();
            Destroy(this);
        }
		
	}
	
}
