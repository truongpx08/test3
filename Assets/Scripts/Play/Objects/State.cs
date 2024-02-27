using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : PlaySubscriber
{
    [SerializeField] protected string currentState;

    [Button]
    protected abstract void SendStateToSubscribers(string value);

    protected void SetCurrentState(string value)
    {
        // Debug.Log("SetCurrentState: " + value);
        this.currentState = value;
    }
}