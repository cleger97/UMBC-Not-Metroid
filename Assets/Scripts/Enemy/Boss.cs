using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public GameObject target;
    public float attackTime;
    public float coolDown;
    public Animator anim;
    [SerializeField]
    private float speed = 3;
    private bool isAttacking = false;
    // Use this for initialization
    void Start () {
        attackTime = 2;
        coolDown = 1.0f;
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        attackTime -= Time.deltaTime;
        if(attackTime > 0)
            Shuffle();
        if(attackTime < 0)
        {
            Attack();
        }
        if(attackTime < -.5)
        {
            attackTime = 2;
        }
    }

    void MoveRight(float s)
    {
        transform.Translate(Vector2.right * s * Time.deltaTime);
    }

    void MoveLeft(float s)
    {
        transform.Translate(Vector2.left * s * Time.deltaTime);
    }

    void Shuffle()
    {
        coolDown -= Time.deltaTime;
        if (coolDown > .5)
            MoveRight(speed);
        if (coolDown < .5)
            MoveLeft(speed);
        if (coolDown < 0)
        {
            coolDown = 1;
        }
    }
    private void Attack()
    {
        MoveRight(speed * 2);
    }
}
