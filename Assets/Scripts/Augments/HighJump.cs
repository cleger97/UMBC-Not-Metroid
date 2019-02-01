using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJump : ActionScript {

    public float newHeight = 3f;
    public override void Action() {
        Player.instance.jumpHeight = 3f;
        Player.instance.UpdateJumpHeight(3f);
    }
	
}
