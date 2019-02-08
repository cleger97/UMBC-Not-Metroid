using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventItemPickup : MonoBehaviour
{
    public GameEvent item;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player")
        {
            item.Raise();
        }
    }


}
