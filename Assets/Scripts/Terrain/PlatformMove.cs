using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float ResetTime = 3f;
    [SerializeField]
    private GameObject enemy;
    private float time;
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    public bool uad, sts, blink, fall, shake, spawn = false;
    public ParticleSystem ps1;
    public Camera cam;

    // Use this for initialization
    void Start () {
        time = ResetTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        if(uad)
            UpAndDown();
        if(blink)
            Blink();
        if(sts)
            SideToSide();
    }
    void SideToSide()
    {
        if (time > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            time -= Time.deltaTime;
            if (time < -ResetTime) { time = ResetTime; }
        }
    }
    void UpAndDown()
    {
        if (time > 0)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            time -= Time.deltaTime;
            if (time < -ResetTime) { time = ResetTime; }
        }
    }
    void Blink()
    {
        if (time > 0)
        {
            bc.enabled = false;
            sr.enabled = false;
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            bc.enabled = true;
            sr.enabled = true;
            time -= Time.deltaTime;
            if (time < -ResetTime) { time = ResetTime; }
        }  
    }
    void Break()
    {
        bc.enabled = false;
        sr.enabled = false ;
    }
    void Spawn()
    {
      var numEnemies = 5;
        for(int i = 0; i < numEnemies; i++)
        {
            Instantiate(enemy, new Vector3(transform.position.x-7+2*i, transform.position.y+10, transform.position.z), Quaternion.identity);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player" && fall)
        {
            Break();
            Instantiate(ps1, transform.position, Quaternion.identity);
        }
        if (col.tag == "Player" && shake)
        {
            cam.GetComponent<RipplePostProcessor>().ripple = true;
            shake = false;
        }
        if (col.tag == "Player" && spawn)
        {
            Spawn();
            spawn = false;
        }
    }
}
