using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {
	// Abstract class for weapons
	// Should be able to "fire" them and get the time before can be fired again
	public abstract void Fire();
	public float damage = 5f;

	public float energyCost = 1f;
	
}
