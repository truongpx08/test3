using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.TestTools;

public class HeroState : State
{
    [SerializeField] private bool isAllowedNextState;
    [SerializeField] private List<StateType> stateOrder;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        SetIsAllowedNextState(true);
        SetStateOrder();
    }

    public enum FeedbackType
    {
        StartState,
        EndState,
    }

    public enum StateType
    {
        // Appear,
        Move,
        Attack,
        // Hurt,
        // Disappear,
    }


    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        SendStateToSubscribers(StateType.Move);
    }

    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        if (this.isAllowedNextState)
            SetCurrentState(value.ToString());
    }

    protected override void SendStateToSubscribers(object value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnHeroStateChange,
            new object[] { (StateType)value }));
    }

    public void GetFeedback(FeedbackType value)
    {
        switch (value)
        {
            case FeedbackType.StartState:
                SetIsAllowedNextState(false);
                break;
            case FeedbackType.EndState:
                SetIsAllowedNextState(true);
                StateType nextState = GetNextState();
                Debug.LogWarning("next state" + nextState.ToString());
                SendStateToSubscribers(nextState);
                break;
        }
    }

    private StateType GetNextState()
    {
        Enum.TryParse(currentState, out StateType item);
        var indexOfCurrentState = this.stateOrder.IndexOf(item);
        return stateOrder[indexOfCurrentState + 1];
    }

    private void SetIsAllowedNextState(bool value)
    {
        this.isAllowedNextState = value;
    }

    private void SetStateOrder()
    {
        this.stateOrder = new List<StateType>((StateType[])Enum.GetValues(typeof(StateType)));
    }
}