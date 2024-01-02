using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class TruongStateMachine : TruongMonoBehaviour
{
    private int currentStateIndex;
    [SerializeField] private string defaultState;
    [SerializeField] private Transform currentState;
    public Transform CurrentState => currentState;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        currentStateIndex = -1;
        SetDefaultState();
        ChangeState(defaultState);
    }

    protected abstract void SetDefaultState();

    protected void SetDefaultState(string stateName)
    {
        this.defaultState = stateName;
    }

    [Button]
    public void ChangeState(string stateName)
    {
        // Debug.Log("Changing state to " + stateName);
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name != stateName)
            {
                DisableOtherState(child);
                continue;
            }

            // Debug.Log("State to " + stateName);
            currentStateIndex = i;
            EnableCurrentState(child);
            SetCurrentState(child);
        }
    }


    public void ChangeState(int stateIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (i != stateIndex)
            {
                DisableOtherState(child);
                continue;
            }

            currentStateIndex = i;

            EnableCurrentState(child);
            SetCurrentState(child);
        }
    }


    [Button]
    public void Next()
    {
        if (currentStateIndex < 0)
        {
            ChangeState(0);
            return;
        }

        var nextStateIndex = currentStateIndex + 1;
        if (nextStateIndex > transform.childCount - 1)
        {
            ChangeState(0);
            return;
        }

        ChangeState(nextStateIndex);
    }

    [Button]
    public void Previor()
    {
        if (currentStateIndex < 0)
        {
            ChangeState(transform.childCount - 1);
            return;
        }

        int previorStateIndex = currentStateIndex - 1;
        if (previorStateIndex < 0)
        {
            ChangeState(transform.childCount - 1);
            return;
        }

        ChangeState(previorStateIndex);
    }


    [Button]
    public void Exit()
    {
        currentStateIndex = -1;
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            DisableOtherState(child);
        }

        SetCurrentState(null);
    }

    private void SetCurrentState(Transform state)
    {
        if (state == this.currentState) return;
        var previorState = this.currentState;
        this.currentState = state;
        OnStateChanged(previorState, this.currentState);
    }


    protected virtual void OnStateChanged(Transform preState, Transform curState)
    {
        // For override
    }

    protected virtual void EnableCurrentState(Transform state)
    {
        if (!state) return;
        state.gameObject.SetActive(true);
    }

    protected virtual void DisableOtherState(Transform state)
    {
        if (!state) return;
        state.gameObject.SetActive(false);
    }
}