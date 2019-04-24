using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSpiderSlamAttack : MonoBehaviour
{
    [SerializeField]
    private NSpiderSlamAttackCollider Slam1;
    [SerializeField]
    private NSpiderSlamAttackCollider Slam2;
    [SerializeField]
    private NSpiderSlamAttackCollider Slam3;

    public float SlamAttackDamage = 50f;

    public float SlamAttackDelay = 2f;

    public float SlamAttackDelayEnraged = 1f;

    // These are wired to animators
    public void SlamAttack1() {
        Slam1.Enable();
    }

    public void SlamAttack2() {
        Slam2.Enable();
    }

    public void SlamAttack3() {
        Slam3.Enable();
    }

    public void SlamFinish() {
        Slam1.Disable();
        Slam2.Disable();
        Slam3.Disable();
    }
}
