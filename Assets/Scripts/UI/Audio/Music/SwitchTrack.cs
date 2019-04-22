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
       
    }

    public void Switch() {
        musicObject = Music.inst;

        if (musicObject != null) {
            musicObject.SwitchTrack(switchTo);
            hasSwitched = true;
        } else {
            Debug.LogWarning("Null music object, no switch");
        }
        
    }

}
