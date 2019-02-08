using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(Collider2D))]
public class DoorCameraSwitch : MonoBehaviour {

	public GameObject VCamLeft;
	public GameObject VCamRight;
	// Use this for initialization
	void Start () {
		if (VCamLeft == null || VCamRight == null) {
			Debug.LogWarning("Missing camera assignment");
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("Trigger entered");
		// if the other object enters from left side
		if (this.transform.position.x > other.transform.position.x) {
			VCamLeft.SetActive(true);
			VCamRight.SetActive(false);
			//VCamLeft.GetComponent<CinemachineVirtualCamera>().m_Priority = 2;
			//VCamRight.GetComponent<CinemachineVirtualCamera>().m_Priority = 1;

		} else {
			VCamLeft.SetActive(false);
			VCamRight.SetActive(true);
			//VCamLeft.GetComponent<CinemachineVirtualCamera>().m_Priority = 1;
			//VCamRight.GetComponent<CinemachineVirtualCamera>().m_Priority = 2;
		}
	}
}
