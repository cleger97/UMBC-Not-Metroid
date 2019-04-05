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
                GameObject instTomake = Instantiate(Resources.Load("Level_Prefabs/Menu UI Canvas")) as GameObject;
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

    private bool allowedToUnpause = true;

   
    public GameObject text;
    public GameObject GameOverScreen;
    public GameObject select;
    public GameObject continueButton;
    public GameObject restartButton;
    public GameObject returnButton;
    public GameObject controlMenu;
    public GameObject musicVolSlider;

    public GameObject SFXVolSlider;

    AudioSource SFX;
    public AudioClip backClip;
    public AudioClip selectClip;

    private MenuSelect selectInst = null;

    void Awake() {
        if (inst != null) {
			Destroy(this.gameObject);
		} else {
            inst = this;
			DontDestroyOnLoad(this);
		}

        

    }

    void Start() {
        selectInst = (MenuSelect) gameObject.GetComponent<MenuSelect>();
        SFX = gameObject.GetComponent<AudioSource>();
        continueButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener (ResumeGame);
        restartButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener (RestartLoad);
        returnButton.transform.GetChild(0).GetComponent<Button>().onClick.AddListener (ReturnLoad);
    }

    void Update() {
        //TODO: Add if at first level, disable pause
        
        if (Input.GetButtonDown("Menu") && allowedToUnpause) {
            if (!paused) {
                SFX.clip = selectClip;
                SFX.Play();
                Pause();
                
            } else {
                SFX.clip = backClip;
                SFX.Play();
                ResumeGame();
            }
        }
    }

    private void Disable() {
        allowedToUnpause = true;
        select.SetActive (false);
		GameOverScreen.SetActive (false);
		continueButton.SetActive (false);
		restartButton.SetActive (false);
		returnButton.SetActive (false);
		text.SetActive (false);
		controlMenu.SetActive (false);
        musicVolSlider.SetActive(false);
        SFXVolSlider.SetActive(false);
    }

    private void Pause() {
        paused = true;  
        allowedToUnpause = true;
        text.SetActive(true);
		text.GetComponent<Text> ().text = "Paused";
		text.GetComponent<Text>().color = Color.blue;

		continueButton.SetActive(true);
        restartButton.SetActive(true);
		returnButton.SetActive(true); 
		//controlMenu.SetActive (true);
        musicVolSlider.SetActive(true); 
        SFXVolSlider.SetActive(true); 

        List<Transform> pauseObjects = new List<Transform>() {continueButton.transform, restartButton.transform, returnButton.transform, musicVolSlider.transform};
        selectInst.Pause(pauseObjects);

		select.SetActive (true);

		Time.timeScale = 0;
    }

    public void GameOver() {
        paused = true;
        allowedToUnpause = false;
        text.SetActive(true);
        text.GetComponent<Text>().text = "Game Over";
        text.GetComponent<Text>().color = Color.red;

        restartButton.SetActive(true);

        List<Transform> pauseObjects = new List<Transform>(){ restartButton.transform };

        selectInst.Pause(pauseObjects);

        select.SetActive(true);
        
        Time.timeScale = 0;
    }

    public void InputButton(string button) {
        //Debug.Log(button);
        if (button.Equals("Restart")) {
            RestartLoad();
        }
        if (button.Equals("Continue")) {
            ResumeGame();
        }
        if (button.Equals("Return")) {
            ReturnLoad();
        }
    }

    public void RestartLoad() {
        Disable();
        LevelTransitionHandler.instance.LoadNewScene(LevelTransitionHandler.lastLoad, SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
        paused = false;
        selectInst.Unpause();
    }

    private void ResumeGame() {
        if (paused == false) { return; }
        Disable();
        paused = false;
        Time.timeScale = 1;
        selectInst.Unpause();
    }

    private void ReturnLoad() {
        Disable();
        LevelTransitionHandler.instance.ReturnToMain();
        Time.timeScale = 1;
        paused = false;
        selectInst.Unpause();
    }
}