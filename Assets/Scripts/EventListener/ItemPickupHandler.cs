using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupHandler : EventListener
{
    // implied List<GameEvent> toListen;

    public override void Invoke(GameEvent e, params Object[] argv)
    {
        switch (e.name)
        {
            case "High Jump":
                {
                    Debug.Log("High Jump acquired");
                    return;
                }
            default:
                return;

        }
    }

}
