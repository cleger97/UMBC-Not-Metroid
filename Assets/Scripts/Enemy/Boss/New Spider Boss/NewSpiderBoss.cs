using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSpiderBoss : MonoBehaviour
{
    // "Hardened Shield" effect: Should take 10 hits, regardless of strength.
    public int health = 10;
    public int maxHealth = 10;
    public float speed;
    // damage is gonna ramp up the lower the boss is
    public float damage;
    public float atkSpd;

    private bool flip = false;
    private float flipTime = 0;

    private int state = 0;


    // boss HP bar
    public Slider healthBar;
    private Player player;
    private Animator anim;

    private int animState = 0;

  // Start is called before the first frame update
  void Start()
    {
        player = Player.instance;
        anim = GetComponent<Animator>();

        health = maxHealth;

        healthBar.maxValue = health;
        healthBar.value = health;

        anim.SetTrigger("Idle");

    }

    private void ResetAnimator() {
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Attack");
        anim.ResetTrigger("Walk");

        anim.SetTrigger("Idle");
        state = 0;
        animState = 0;
    }

    private void UpdateAnimator(int state) {
        if (animState == state) {
            return;
        }
        switch (state) {
            case 0: anim.SetTrigger("Idle"); break;
            case 1: anim.SetTrigger("Walk"); break;
            case 2: anim.SetTrigger("Attack"); break;
            default: break;
        }
        animState = state;
        return;
    }

    /*
    State System
    State 0: Idle State
    State 1: Walk at player
    State 2: Slam Attack
    State 3: Jump Attack (?)
    State 4: Shooting Attack
    */
    void Update()
    {
        if (player == null) {return;}
        if (MenuHandle.instance != null && MenuHandle.instance.isPaused()) { return; }

        UpdateAnimator(state);
        switch (state) {
            // idle state
            case 0: {
                
                break;
            }
            // walk at player
            case 1: {

                break;
            }
            // slam attack
            case 2: {
                break;
            }

            default: {

                break;
            }
        }
    }
}
