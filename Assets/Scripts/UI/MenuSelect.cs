using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour {

    public Transform select;
    int currentlySelected = 0;
	List<Transform> objects = null;

    AudioSource SFX;
    public AudioClip switchClip;
    public AudioClip backClip;
    public AudioClip selectClip;

    

    bool isPaused;

    float maxInputTimer = 0.2f;
    float inputTimer = 0f;

    public void Start() {
        SFX = gameObject.GetComponent<AudioSource>();
    }

	public void Pause(List<Transform> objects) {
        this.objects = objects;
        isPaused = true;
    }

    public void Unpause() {
        isPaused = false;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(currentlySelected);
        if (!isPaused) { return; }
        if (objects == null) { return; }

        if (Input.GetButtonDown("Submit") ) {
            SFX.clip = selectClip;
            SFX.Play();
            MenuHandle.instance.InputButton(objects[currentlySelected].name);
        }

        if (inputTimer > 0f) {
            inputTimer -= Time.unscaledDeltaTime;
            if (inputTimer < 0f) {
                inputTimer = 0f;
            }
            return;
        }

        int vert = (int) Input.GetAxisRaw("Vertical");
        if (vert == 0) { return; }

        SFX.clip = switchClip;

        if (vert < 0) {
            currentlySelected--;
            if (currentlySelected < 0) { 
                currentlySelected = objects.Count - 1;
            }
            select.position = objects[currentlySelected].position;
        } else if (vert > 0) {
            currentlySelected++;
            if (currentlySelected > objects.Count - 1) { 
                currentlySelected = 0;
            }
            select.position = objects[currentlySelected].position;
        }

        SFX.Play();

        inputTimer = maxInputTimer;
        return;
		
	}
}
