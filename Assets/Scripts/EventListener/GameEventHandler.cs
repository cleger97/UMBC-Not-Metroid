using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventHandler : MonoBehaviour
{
    public virtual void Invoke()
    {
        Invoke(null);
    }

    public abstract void Invoke(GameEvent e); 
}
