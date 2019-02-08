using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class EventListener : MonoBehaviour
{
    public List<GameEvent> toListen;

    void OnEnable()
    {
        foreach(GameEvent e in toListen)
        {
            e.RegisterListener(this);
        }
        
    }

    void OnDisable()
    {
        foreach (GameEvent e in toListen)
        {
            e.UnregisterListener(this);
        }
    }

    public abstract void Invoke(GameEvent e,  params Object[] argv);

}
