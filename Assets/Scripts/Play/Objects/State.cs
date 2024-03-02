using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : PlaySubscriber
{
    [SerializeField] protected string currentState;

    [Button]
    protected abstract void NotifyToSubscribers(string value);

    protected void SetCurrentState(string value)
    {
        this.currentState = value;
    }
}