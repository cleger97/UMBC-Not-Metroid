using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    public string eventName;

    public List<EventListener> eListeners = new List<EventListener>();

    public void RegisterListener(EventListener e) {
        eListeners.Add(e);
    }

    public void UnregisterListener(EventListener e)
    {
        eListeners.Remove(e);
    }

    public void Raise()
    {
        foreach (EventListener e in eListeners)
        {
            e.Invoke(this);
        }
    }
}
