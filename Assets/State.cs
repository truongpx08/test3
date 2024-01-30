using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : GameObj
{
    [SerializeField] protected string currentState;

    [Button]
    protected abstract void SendStateToSubscribers(string value);

    protected void SetCurrentState(string value)
    {
        this.currentState = value;
    }
}