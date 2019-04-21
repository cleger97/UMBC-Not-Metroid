﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewSpiderBoss : MonoBehaviour
{
    
    public float speed = 5;
    // damage is gonna ramp up the lower the boss is
    public float damage;
    public float atkDelay = 0f;
    [SerializeField]
    private float maxAtkDelay = 5f;

    public bool isThreatened = false;
    public bool isAttacking = false;

    private bool flip = false;
    private float flipTime = 0;

    private bool isFacingLeft = true;

    private bool isEnraged = false;

    [SerializeField]
    private int state = 0;

    [SerializeField]
    private BoxCollider2D attackCollider;

    [SerializeField]
    private BoxCollider2D detectionCollider;

    public float flipDelay = 1f;

    public float MAXFlipDelay = 1f;

    
    private Player player;
    private Animator anim;

    private int animState = 0;

  // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
        anim = GetComponent<Animator>();

        

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

    private void CheckDetectors() {
        if (player == null) {
            Debug.LogError("No player");
            return;
        }
        if(detectionCollider == null) {
            Debug.LogWarning("No Detedtion Collider; won't enter threatened state");
            return;
        }
        bool isDetected = detectionCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds);
        isThreatened = isDetected;

        if (attackCollider == null) {
            Debug.LogWarning("No Attack Collider; won't attack");
            return;
        }
        bool isAttackCollide = attackCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds);
        isAttacking = isAttackCollide;

        

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

        if (atkDelay > 0) {
            atkDelay -= Time.deltaTime;
            atkDelay = (atkDelay >= 0) ? atkDelay : 0;
            if (atkDelay == 0) {
                ResolveAttack();
            }
        }

        UpdateAnimator(state);
        CheckDetectors();
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
                SwitchTrack trackSwitcher = this.gameObject.GetComponent<SwitchTrack>();
                if (trackSwitcher != null && !trackSwitcher.hasSwitched) {
                    trackSwitcher.Switch();
                }

                // walk at player
                Vector3 direction = player.transform.position - this.transform.position;

                bool flipRight = (player.transform.position.x + 1 > transform.position.x);

                bool flipLeft = (player.transform.position.x - 1 < transform.position.x);

                float xDirection = (isFacingLeft) ? -1 : 1;

                if (isFacingLeft && flipRight) {
                    if (flipDelay <= 0) {
                        Quaternion rot = new Quaternion(0, 180, 0, 0);
                        transform.rotation = rot;
                        isFacingLeft = false;
                        flipDelay = MAXFlipDelay;
                    } else {
                        flipDelay -= Time.deltaTime;
                    }
                    // Flip
                    
                }                
                else if (!isFacingLeft && flipLeft) {
                        // Flip
                    if (flipDelay <= 0){
                        Quaternion rot = new Quaternion(0, 0, 0, 0);
                        transform.rotation = rot;
                        isFacingLeft = true;
                        flipDelay = MAXFlipDelay;
                    } else {
                        flipDelay -= Time.deltaTime;
                    }
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
                if (atkDelay == 0) {
                    atkDelay = maxAtkDelay;
                }

                break;
            }

            default: {

                break;
            }
        }
    }

    public void Enrage() {
        if (isEnraged) {
            return;
        }
        isEnraged = true;

        anim.speed = 1.5f;
        speed = 1.5f * speed;
        maxAtkDelay = 1.5f;
        this.GetComponent<SpriteRenderer>().color = Color.blue;
    }
    public void ResolveAttack() {
        Debug.Log("Resolve called");
        if (isThreatened) {
            state = 1;
        } else {
            state = 0;
        }
        isAttacking = false;
    }


    public void DebugLog() {
        Debug.Log("Debug animator event fired");
    }
}