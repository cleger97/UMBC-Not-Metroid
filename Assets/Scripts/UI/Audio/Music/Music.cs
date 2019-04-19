using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour {

    public static Music inst = null;

    public AudioSource audio;

    private bool detonate = false;
    
    void Awake() {
        if (inst == null)
        {
            DontDestroyOnLoad(this.gameObject);
            inst = this;
        }
        else
        { 
            detonate = true;
        }
    }

	// Use this for initialization
	void Start () {        
        audio = gameObject.GetComponent<AudioSource>();

        if (detonate) {
            if (Music.inst.audio.clip != this.audio.clip) {
                Music.inst.audio.clip = this.audio.clip;
                Music.inst.audio.Play();
            }

            audio.Stop();
            Destroy(this);
        }
		
	}

    public void SwitchTrack(AudioClip clip) {
        audio.clip = clip;
        audio.Play();
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
