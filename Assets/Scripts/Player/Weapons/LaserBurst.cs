using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LaserBurst : Weapon {

	public GameObject weapon;

	public float weaponCD = 1f;
	private float currentWeaponCD = 0;

	public float projectileSpeed = 8f;

	public float projectileTime = 2f;
	private bool readyToFire = true;


	void Awake() {
        
	}

	void Update() {
		if (currentWeaponCD == 0) {
			readyToFire = true;
		}

		if (currentWeaponCD > 0) {
			currentWeaponCD -= Time.deltaTime;
			if (currentWeaponCD < 0) { currentWeaponCD = 0; }
		}
	}

    public void ProjectileSpawn()
    {
        Vector3 currentPos = transform.position;
        GameObject laser = Instantiate(weapon, new Vector3(currentPos.x + .1f, currentPos.y - 0.5f, currentPos.z), Quaternion.identity, null);
        laser.SetActive(true);

        laser.GetComponent<LaserBeam>().SetDespawnTimer(projectileTime);
        float directionX = Mathf.Sign(Player.instance.transform.position.x - this.transform.position.x) * -1;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2((projectileSpeed * directionX), 0);
    }
	// really basic sword swing - just activate the box, but at some point an animation will be attached.
	public override bool Fire() {

		if (readyToFire) {
			
			Vector3 currentPos = transform.position;

			//GameObject laser = Instantiate(weapon, new Vector3(currentPos.x +.1f, currentPos.y -0.5f, currentPos.z), Quaternion.identity, null);

			// TODO: Set up vertical angles.
			float directionX = Mathf.Sign( Player.instance.transform.position.x - this.transform.position.x ) * -1;
			//float directionY = Input.GetAxis("Vertical");

			//laser.SetActive(true);

			//laser.GetComponent<LaserBeam>().SetDespawnTimer(projectileTime);

			//laser.GetComponent<Rigidbody2D>().velocity = new Vector2((projectileSpeed * directionX), 0);

			// and we fired, wait for the next one.
			readyToFire = false;
			currentWeaponCD = weaponCD;

			return true;
		} else {

			return false;
		}
	}


}
