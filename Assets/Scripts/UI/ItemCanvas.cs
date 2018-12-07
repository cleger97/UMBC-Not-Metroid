using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCanvas : MonoBehaviour {

	// Use this for initialization
	GameObject itemCanvas;
	
	string title;
	string desc;
	void Start () {
		itemCanvas = this.gameObject;
	}
	
	public void StartText(string text1, string text2) {
		title = text1;
		desc = text2;
		StartCoroutine(UpdateText());
	}

	public IEnumerator UpdateText() {
		
		itemCanvas.SetActive(true);
		itemCanvas.transform.GetChild(0).gameObject.SetActive(true);
		itemCanvas.transform.GetChild(0).GetComponent<Text>().text = title;
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < 3f) {
			yield return null;
		}
		itemCanvas.transform.GetChild(1).gameObject.SetActive(true);
		itemCanvas.transform.GetChild(1).GetComponent<Text>().text = desc;
		startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < 5f) {
			yield return null;
		}
		itemCanvas.SetActive(false);

	}

}
