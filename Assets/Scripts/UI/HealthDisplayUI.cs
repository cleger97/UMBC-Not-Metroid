using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplayUI : MonoBehaviour {

    // Handles the health/energy display at the top left.
    public Animator camShake;
    public Slider healthBar;
    public Slider energyBar;
    public PlayerHP player;
    public PlayerWeapon pweapon;

    // Use this for initialization
    void Start () {
        player = Player.instance.gameObject.GetComponent<PlayerHP>();
        pweapon = Player.instance.gameObject.GetComponent<PlayerWeapon>();

        if (player == null || pweapon == null) {
            Debug.LogError("Player HP or Weapon not initialized");
            Destroy(this);
        }

        if (healthBar == null && transform.childCount > 0)
        {
            healthBar = transform.GetChild(0).gameObject.GetComponent<Slider>();
            if (healthBar.name != "Health Slider")
            {
                Debug.LogError("Health bar incorrectly init");
            }
        }
        if (energyBar == null && transform.childCount > 1)
        {
            energyBar = transform.GetChild(1).gameObject.GetComponent<Slider>();
            if (energyBar.name != "Energy Slider")
            {
                Debug.LogError("Energy bar incorrectly init");
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        healthBar.value = player.currentHP / player.maxHP;
        energyBar.value = pweapon.currentEnergy / pweapon.maxEnergy;
	}
}
