using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {

    public float maxHP = 250f;
    public float currentHP;

    public bool isDead = false;

    public void Start() {
        RefreshPlayer();
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

        // visual FX for taking damage
    }
}
