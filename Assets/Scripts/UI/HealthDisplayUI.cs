using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplayUI : MonoBehaviour {
    public Animator camShake;
    public Slider healthBar;
    [SerializeField]
    public PlayerHP player;

	// Use this for initialization
	void Start () {
        player = Player.instance.gameObject.GetComponent<PlayerHP>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.value = player.currentHP / player.maxHP;
        //camShake.Play();
        //for testing
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    health--;
            
        //    camShake.Play("CameraShakeAnim");
        //}



	}
}
