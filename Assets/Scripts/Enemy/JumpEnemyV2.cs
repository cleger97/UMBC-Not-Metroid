using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyV2 : MonoBehaviour {
    public float aggroRange = 5f;
    public float moveSpeed = 5f;
    public float jumpTime = 2f;
    public float jumpHeight = 3f;
    public float timeToJumpApex = .3f;
    private float jumpVelocity;
    private GameObject player;
    private Controller2D controller;
    private BoolTimer jumpTimer;
    private BoolTimer updateInput;
    private float gravity;
    private Vector2 velocity;
	// Use this for initialization
	void Start () {
        controller = GetComponent<Controller2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        jumpTimer = gameObject.AddComponent<BoolTimer>().Constructor(true);
        updateInput = gameObject.AddComponent<BoolTimer>().Constructor(true);

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 playerDirection = player.transform.position - transform.position;
        if (playerDirection.magnitude < aggroRange)
        {
            if (updateInput.Value)
            {
                velocity.x = Mathf.Sign(playerDirection.x) * moveSpeed;
                updateInput.UpdateValue(0.5f, false);
            }
            if (jumpTimer.Value)
            {
                velocity.y = jumpVelocity;
                jumpTimer.UpdateValue(jumpTime, false);
            }

        } else {
            if (controller.collisions.below)
            {
                velocity.x = 0;

            }
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
	}
}
