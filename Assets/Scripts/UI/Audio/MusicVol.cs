using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVol : MonoBehaviour
{
    AudioVolumeController AVInst;

    void Start()
    {
        if (AudioVolumeController.inst != null) {
            AVInst = AudioVolumeController.inst;
        }
        AVInst.RegisterAudio(AudioType.Music, this.gameObject);
    }

}
