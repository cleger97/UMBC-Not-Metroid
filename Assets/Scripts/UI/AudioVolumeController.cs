using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour {

    public static float MusicVolPercent = 1;
    public static float SFXVolPercent = 1;

    public Slider musicSlider;

    public Slider SFXSlider;

    public static AudioVolumeController inst = null;

    void Awake() {
        if (inst == null) {
            inst = this;
        }
    }

    void Start() {

    }

    public void UpdateMusicVol() {
        float percent = musicSlider.value;
        AudioVolumeController.MusicVolPercent = percent;
    }

    public void UpdateSFXVol() {
        float percent = SFXSlider.value;
        AudioVolumeController.SFXVolPercent = percent;
    }


}
