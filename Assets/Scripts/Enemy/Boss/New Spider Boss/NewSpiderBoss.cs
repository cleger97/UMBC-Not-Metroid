using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSpiderBoss : MonoBehaviour
{
    // "Hardened Shield" effect: Should take 10 hits, regardless of strength.
    public int health = 10;
    public int maxHealth = 10;
    public float speed = 5;
    // damage is gonna ramp up the lower the boss is
    public float damage;
    public float atkSpd;

    public bool isThreatened = false;

    public bool isAttacking = false;

    private bool flip = false;
    private float flipTime = 0;

    private bool isFacingLeft = true;

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

        if (healthBar != null) {
            healthBar.maxValue = health;
            healthBar.value = health;
        }
        

        ResetAnimator();

        isThreatened = false;

        isFacingLeft = (transform.rotation.y == 180) ? false : true;

    }

    private void ResetAnimator() {
        anim.SetBool("Idle", false);
        anim.ResetTrigger("Attack");
        anim.SetBool("Walk", false);

        //anim.SetBool("Idle", true);
        //state = 0;
        //animState = 0;
    }

    private void UpdateAnimator(int state) {
        if (animState == state) {
            return;
        }
        ResetAnimator();
        switch (state) {
            case 0: anim.SetBool("Idle", true); break;
            case 1: anim.SetBool("Walk", true); break;
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
                // check if threatened
                // if so, move towards threat
                if (isThreatened) {
                    state = 1;
                }
                break;
            }
            // walk at player
            case 1: {
                // walk at player
                Vector3 direction = - this.transform.position + player.transform.position;
                float xDirection = Mathf.Sign(direction.x);

                if (isFacingLeft && (xDirection == 1)) {
                    // Flip
                    Quaternion rot = new Quaternion(0, 180, 0, 0);
                    transform.rotation = rot;
                    isFacingLeft = false;
                }                
                else if (!isFacingLeft && (xDirection == -1)) {
                    // Flip
                    Quaternion rot = new Quaternion(0, 0, 0, 0);
                    transform.rotation = rot;
                    isFacingLeft = true;
                }

                Vector3 newPosition = new Vector3(xDirection * speed * Time.deltaTime + transform.position.x, transform.position.y, transform.position.z);
                transform.position = newPosition;
                
                if (!isThreatened) {
                    state = 0;
                }

                if (isAttacking) {
                    state = 2;
                }

                break;
            }
            // slam attack
            case 2: {
                // Start attack
                // When finished will handle itself

                break;
            }

            default: {

                break;
            }
        }
    }

    public void StartAttack() {

    }

    public void ResolveAttack() {
        if (isThreatened) {
            state = 1;
        } else {
            state = 0;
        }
        isAttacking = false;
    }

}
