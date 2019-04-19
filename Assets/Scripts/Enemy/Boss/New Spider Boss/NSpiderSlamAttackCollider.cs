using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSpiderSlamAttackCollider : MonoBehaviour
{
    [SerializeField]
    private NSpiderSlamAttack attackMain;

    [SerializeField]
    private BoxCollider2D thisCollider;

    [SerializeField]
    private BoxCollider2D manualCollider;

    private bool hitOnThisInstance = false;

    void Start() {
        if (attackMain == null) {
            attackMain = this.transform.parent.GetComponent<NSpiderSlamAttack>();
        }

        if (thisCollider == null) {
            thisCollider = this.GetComponent<BoxCollider2D>();
        }
    }

    public void Enable() {
        hitOnThisInstance = false;
        thisCollider.enabled = true;
    }

    public void Disable() {
        thisCollider.enabled = false;
    }


    void Update() {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.tag == "Player" && !hitOnThisInstance) {
            Player.instance.GetComponent<PlayerHP>().TakeDamage(attackMain.SlamAttackDamage);
            hitOnThisInstance = true;
        }

    }
}
