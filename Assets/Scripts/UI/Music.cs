using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public static Music inst = null;

    AudioSource audio;
    
	// Use this for initialization
	void Start () {        
        audio = gameObject.GetComponent<AudioSource>();

        if (inst == null) {
            DontDestroyOnLoad(this);
            inst = this;
        } else {
            audio.Stop();
            Destroy(this);
        }
		
	}

    void Update() {
        if (AudioVolumeController.inst != null) {
            audio.volume = AudioVolumeController.MusicVolPercent;
        }
        
    }
	
}
