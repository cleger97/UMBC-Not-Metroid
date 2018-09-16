using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class AugmentFunctionList : MonoBehaviour {

	public void LaserSwordAugment() {
        return;
    }

    public void ActivatePrimaryWeapons() {
        Player.instance.Activate(0);
    }
}
