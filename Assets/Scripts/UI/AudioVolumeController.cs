using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AudioType {SFX, Music};

public class AudioVolumeController : MonoBehaviour {

    public static float MusicVolPercent = 1;
    public static float SFXVolPercent = 1;

    public Slider musicSlider;

    public Slider SFXSlider;

    public static AudioVolumeController inst = null;

    public static bool pulse = false;

    private float maxPulse = 0.1f;
    private float currentPulse = 0f;

    private List<GameObject> SFXList;
    private List<GameObject> MusicList;

    void Awake() {
        if (inst == null) {
            inst = this;
        }
    }

    void Start() {
        SFXList = new List<GameObject>();
        MusicList = new List<GameObject>();
    }
    void Update() {
        if (pulse) {
            currentPulse -= Time.unscaledDeltaTime;
            if (currentPulse <= 0) {
                currentPulse = 0f;
                pulse = false;
            }
        }
    }

    public void AddPulse(AudioType audio, GameObject container) {
        if (audio == AudioType.SFX) {
            SFXList.Add(container);
        } else if (audio == AudioType.Music) {
            MusicList.Add(container);
        }
    }

    // Pulse is designed so that you can register audio into the Audio Controller
    // Should allow for better audio control overall
    private void Pulse(AudioType pulse) {
        switch (pulse) {
            case (AudioType.Music): {
                foreach (GameObject container in MusicList) {
                    AudioSource musObj = container.GetComponent<AudioSource>();
                    if (musObj != null) {
                        musObj.volume = MusicVolPercent;
                    }
                }
                break;
            }

            case (AudioType.SFX): {
                foreach (GameObject container in SFXList) {
                    AudioSource SFXObj = container.GetComponent<AudioSource>();
                    if (SFXObj != null) {
                        SFXObj.volume = SFXVolPercent;
                    }
                }
                break;
            }


            default: {
                break;
            }
        }
    }

    public void UpdateMusicVol() {
        float percent = musicSlider.value;
        AudioVolumeController.MusicVolPercent = percent;
        pulse = true;
        currentPulse = maxPulse;
        Pulse(AudioType.Music);
    }

    public void UpdateSFXVol() {
        float percent = SFXSlider.value;
        AudioVolumeController.SFXVolPercent = percent;
        pulse = true;
        currentPulse = maxPulse;
        Pulse(AudioType.SFX);
    }


}
