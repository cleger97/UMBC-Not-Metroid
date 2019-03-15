using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerWeapon : MonoBehaviour {

	[HideInInspector] public List<GameObject> listOfWeapons;
	public GameObject weapon;
	[HideInInspector]public int currentWeapon;
	public float maxEnergy;
	public float currentEnergy;
	public float energyRegen;

	public float swingTime;


	void Start() {
		listOfWeapons = new List<GameObject>();
		foreach (Transform t in transform) {
			if (t.tag != "Weapon") continue;
			Debug.Log("Adding GameObject " + t.name + " to weapon list.");
			listOfWeapons.Add(t.gameObject);
		}

		weapon = listOfWeapons[0];
		currentWeapon = 0;
		if (weapon == null) { 
			Debug.LogError("Weapon initialized to null in PlayerWeapon.");
		}
	}

	// Update is called once per frame
	void Update () {
		if (currentEnergy < maxEnergy) {
			currentEnergy += energyRegen * Time.deltaTime;
			
			if (currentEnergy > maxEnergy) { currentEnergy = maxEnergy; }
		}

		// Tell the weapon to fire.
		// Weapon handles swing times and such, this is just passing the message.
		if (Input.GetButtonDown("Fire1") ) {
			Weapon selectedW = weapon.GetComponent<Weapon>();
			if (selectedW == null) { 
				Debug.LogError("Selected weapon is null");
				return; 
			}

			if (selectedW.energyCost > currentEnergy) {
				return;
			} else {

				// if it fired successfully then deduct energy
				if (selectedW.Fire()) {
					currentEnergy -= selectedW.energyCost;
				}
				
				Debug.Log("Current Energy Left: " + currentEnergy);
				
			}
		}

		if (Input.GetButtonDown("Fire2") ) {
			Debug.Log("Weapon Switch fired");

			if (currentWeapon + 1 == listOfWeapons.Count) {
				currentWeapon = 0;
			} else {
				currentWeapon++;
			}
			weapon = listOfWeapons[currentWeapon];

			weapon.SetActive(true);
		}
	}
}
