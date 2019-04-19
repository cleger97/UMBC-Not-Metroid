using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : MonoBehaviour
{
    [SerializeField]
    private Music musicObject;

    public bool hasSwitched = false;
    public AudioClip switchTo;
    // Start is called before the first frame update
    void Start()
    {
       musicObject = Music.inst;
    }

    public void Switch() {
        musicObject.SwitchTrack(switchTo);
        hasSwitched = true;
    }

}
