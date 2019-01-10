using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon {

	public GameObject weapon;
	public float weaponCD = 0.2f;

	private float currentWeaponCD = 0;
	void Awake() {
		
		DisableWeapon();
	}

	void Update() {
		if (currentWeaponCD == 0) {
			DisableWeapon();
		}

		if (currentWeaponCD > 0) {
			currentWeaponCD -= Time.deltaTime;
			if (currentWeaponCD < 0) { currentWeaponCD = 0; }
		}
	}

	void DisableWeapon() {
		weapon.GetComponent<SpriteRenderer>().enabled = false;
		weapon.GetComponent<BoxCollider2D>().enabled = false;
	}

	// really basic sword swing - just activate the box, but at some point an animation will be attached.
	public override void Fire() {
		if (currentWeaponCD <= 0) {
			weapon.GetComponent<SpriteRenderer>().enabled = true;
			weapon.GetComponent<BoxCollider2D>().enabled = true;
			currentWeaponCD = weaponCD;
		}

	}
}
