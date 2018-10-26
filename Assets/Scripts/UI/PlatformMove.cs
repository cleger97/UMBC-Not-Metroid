using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour {
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float ResetTime = 3f;
    private float time;
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    public bool uad, sts, b = false;
	// Use this for initialization
	void Start () {
        time = ResetTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 pos = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z);
        if(uad)
            UpAndDown();
        if(b)
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
}
