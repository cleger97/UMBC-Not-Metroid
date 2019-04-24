using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NSpiderBossHealth : MonoBehaviour
{
    // "Hardened Shield" effect: Should take 10 hits, regardless of strength.
    public int health = 10;
    public int maxHealth = 10;

    [SerializeField]
    private NewSpiderBoss boss;


    public ParticleSystem ps2;
    // Start is called before the first frame update

    // boss HP bar
    public Slider healthBar;
    void Start()
    {
        health = maxHealth;
        if (healthBar != null) {
            healthBar.maxValue = health;
            healthBar.value = health;
        }

        if (boss == null) {
            boss = this.GetComponent<NewSpiderBoss>();
        }
    }

    public void ReceiveHit(float damage) {
        // boss should be pretty resilient
        damage = ( (int) (damage / 5) > 0) ? (int) (damage / 5) : 1;

        health -= (int) damage;

        if (health < 0) {
            health = 0;
        }

        if (health <= 0) {
            // death stuff
            // explode into particles
            Instantiate(ps2, transform.position, Quaternion.identity);
            MenuHandle.instance.Victory();
            Destroy(gameObject);
        }

        if ((float) health / (float) maxHealth < 0.4) {
            boss.Enrage();
        }

        if (healthBar == null) {return;}
        healthBar.value = health;
    }
}
