﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScene : MonoBehaviour {
	public static GlobalScene instance {
		get {
			if (inst == null) {
				GameObject instanceToMake = Instantiate(Resources.Load("Prefabs/Global")) as GameObject;
				instanceToMake.GetComponent<GlobalScene>().Load();
				return inst;
			} else {
				return inst;
			}
		} 

		set {
			inst = value;
		}
	}

	private static GlobalScene inst = null;
	// Use this for initialization
	public static GlobalScene CreateInstance() {
		GameObject instanceToMake = Instantiate(Resources.Load("Prefabs/Global")) as GameObject;
		instanceToMake.GetComponent<GlobalScene>().Load();
		return inst;
	}

	void Awake () {
		if (inst != this && inst != null) {
			Destroy(this.gameObject);
		} else {
			DontDestroyOnLoad(this);
		}


	}

	void Load() {
		if (inst != this && inst != null) {
			Destroy(this.gameObject);
		} else {
			DontDestroyOnLoad(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}