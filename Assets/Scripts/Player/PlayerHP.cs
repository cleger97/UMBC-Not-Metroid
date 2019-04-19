using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    public float maxHP = 250f;
    public float currentHP;

    public float hpRegenRate = 5f;

    private float timeSinceLastHit = 0f;

    public float timeUntilFastRegen = 8f;

    public bool isDead = false;

    public void Start() {
        RefreshPlayer();
    }

    public void Update() {
        if (timeSinceLastHit < timeUntilFastRegen) {
            timeSinceLastHit += Time.deltaTime;
        }

        if (!isDead && currentHP < maxHP) {
            if (timeSinceLastHit > timeUntilFastRegen) {
                currentHP += hpRegenRate * 5 * Time.deltaTime;
            } else if (timeSinceLastHit > 1f) {
                currentHP += hpRegenRate * Time.deltaTime;
            }   
        }

        currentHP = (currentHP > maxHP) ? maxHP : currentHP;

    }

    public void RefreshPlayer() {
        currentHP = maxHP;
        isDead = false;
    }

    public float GetCurrentHP() {
        return currentHP;
    }

    private void Die() {
      MenuHandle.instance.GameOver();

    }

    public void TakeDamage (float damage) {
        if (currentHP > 0) {
            currentHP -= damage;
        }

        currentHP = (currentHP < 0) ? 0 : currentHP;

        if (currentHP <= 0) {
            Debug.LogWarning("Player died");
            isDead = true;
            Die();
        }

        timeSinceLastHit = 0f;

        // visual FX for taking damage
    }
}
