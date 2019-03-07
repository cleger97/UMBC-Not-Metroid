using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRegenPickup : MonoBehaviour
{
    private float regenTime;
    [SerializeField] private PlayerWeapon regen;
    private AudioSource source;
    private SpriteRenderer sr;
    private bool pickedUp = false;
    public float regenBoostTime;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        source = FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        regenTime -= Time.deltaTime;
        if (regenTime < 0 && pickedUp)
        {
            regen.energyRegen = 1;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            source.Play();
            pickedUp = true;
            sr.enabled = false;
            regenTime = regenBoostTime;
            regen.energyRegen *= 15f;
        }
    }
}
