using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXVol : MonoBehaviour
{
    AudioVolumeController AVInst;

    void Start()
    {
        if (AudioVolumeController.inst != null) {
            AVInst = AudioVolumeController.inst;
        }
        AVInst.RegisterAudio(AudioType.SFX, this.gameObject);
    }

}
