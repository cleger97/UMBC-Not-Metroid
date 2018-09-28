using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplayUI : MonoBehaviour {
    public Animator camShake;
    public Slider healthBar;
    [SerializeField]
    private int health = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.value = health;
       // camShake.Play();
        //for testing
        if (Input.GetKeyDown(KeyCode.P))
        {
            health--;
            camShake.Play("CameraShakeAnim");
        }
	}
}
