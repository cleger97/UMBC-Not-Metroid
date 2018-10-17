using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHandle : MonoBehaviour {

    public static MenuHandle instance {
        get {
            if (inst == null) {
                GameObject instTomake = Instantiate(Resources.Load("Prefabs/UI Canvas")) as GameObject;
                DontDestroyOnLoad(inst);
                inst = instTomake.GetComponent<MenuHandle>();
                return inst;
            } else {
                return inst;
            }
        }
        set {
            inst = value;
        }
    }

    private static MenuHandle inst = null;

    private bool paused = false;

   
    public GameObject text;
    public GameObject GameOverScreen;
    public GameObject select;
    public GameObject continueButton;
    public GameObject restartButton;
    public GameObject returnButton;
    public GameObject controlMenu;



    void Awake() {
        if (inst != null) {
			Destroy(this.gameObject);
		} else {
            inst = this;
			DontDestroyOnLoad(this);
		}

        

    }

    void Start() {
        restartButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener (RestartLoad);
    }

    void Update() {
        //TODO: Add if at first level, disable pause
        if (Input.GetButtonDown("Menu")) {
            if (!paused) {
                Pause();
            } else {
                ResumeGame();
            }
        }
    }

    private void Disable() {
        select.SetActive (false);
		GameOverScreen.SetActive (false);
		continueButton.SetActive (false);
		restartButton.SetActive (false);
		returnButton.SetActive (false);
		text.SetActive (false);
		controlMenu.SetActive (false);
    }

    private void Pause() {
        paused = true;  
        text.SetActive(true);
		text.GetComponent<Text> ().text = "Paused";
		text.GetComponent<Text>().color = Color.blue;

		continueButton.SetActive(true);
        restartButton.SetActive(true);
		returnButton.SetActive(true);
		//controlMenu.SetActive (true); 
		
		select.SetActive (true);

		Time.timeScale = 0;


    }

    public void RestartLoad() {
        Disable();
        LevelTransitionHandler.instance.LoadNewScene(LevelTransitionHandler.lastLoad, SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    private void ResumeGame() {
        if (paused == false) { return; }
        Disable();
        paused = false;
        Time.timeScale = 1;
    }
}