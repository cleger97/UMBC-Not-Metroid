using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public static Music inst = null;

    public AudioSource audio;
    
	// Use this for initialization
	void Start () {        
        audio = gameObject.GetComponent<AudioSource>();

        if (inst == null) {
            DontDestroyOnLoad(this);
            inst = this;
        } else {
            if (Music.inst.audio.clip != this.audio.clip) {
                Music.inst.audio.clip = this.audio.clip;
                Music.inst.audio.Play();
            }

            audio.Stop();
            Destroy(this);
        }
		
	}

    void Update() {
        if (AudioVolumeController.inst != null) {
            audio.volume = AudioVolumeController.MusicVolPercent;
        }
        
    }

    public void Toggle() {
        if (audio.isPlaying) {
            audio.Pause();
        } else {
            audio.Play();
        }
        
    }
	
}
