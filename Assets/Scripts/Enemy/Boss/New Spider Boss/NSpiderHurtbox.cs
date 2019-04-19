using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NSpiderHurtbox : MonoBehaviour
{
    public ParticleSystem ps1;

    public float damageMultiplier = 1f;
    [SerializeField]
    private NSpiderBossHealth spiderHP;

    void Start() {
        if (spiderHP == null) {
            spiderHP = this.transform.parent.GetComponent<NSpiderBossHealth>();
        }
    }
    void OnTriggerEnter2D(Collider2D collider) {

        Vector2 pos = new Vector2(collider.transform.position.x, collider.transform.position.y);
        if (collider.tag == "Weapon") {
            Debug.Log("Hit");
            float damage = Player.instance.GetWeaponDamage();
            damage *= damageMultiplier;

            spiderHP.ReceiveHit(damage);

            if (ps1 != null) {
                Instantiate(ps1, pos, Quaternion.identity);
            }
            
            
        }
    }
}
