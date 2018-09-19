using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    // Container for Weapons and Firing
    enum PrimaryWeapons {Beam, LaserSword}
    bool[] PrimaryWeaponsUnlocked = {true, false};
    enum SecondaryWeapons{Missiles, Grenades}
    bool[] SecondaryWeaponsUnLocked = {false, false};
    bool hasPrimaryWeapon;
    bool hasSecondaryWeapon;

    int currentPrimaryWeapon = 0;
    int currentSecondaryWeapon = -1;

    public void Activate (int id) {
        switch (id) {
            case 0: {hasPrimaryWeapon = true; return;}


            default: { Debug.Log("Invalid Activate"); return;}
        }
    }
    public bool PrimaryAttack() {
        if (!hasPrimaryWeapon) {
            Debug.Log("Player cannot attack without primary weapon!");
            //TODO: Sound/Text that indicates not unlocked
            return false;
        }
        
        //TODO: Actual fire.
        switch(currentPrimaryWeapon) {
            case 0: {
                // do beam stuff
                break;
            }
            case 1: {
                // do laser sword stuff
                break;
            }
            default: {
                break;
            }
        }

        return true;
    }

}
