using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class HeroState : State
{
    [SerializeField] private List<StateType> stateOrder;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        SetStateOrder();
    }

    public enum StateType
    {
        BeforeMove,
        Move,
        Attack,
        Injury,
        Faint,
    }

    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        SetCurrentState(value.ToString());
    }

    protected override void OnGameStateChange(string value)
    {
        base.OnGameStateChange(value);
        if (value != GameState.OnUpdate) return;
        CheckNextState();
    }

    [Button]
    private void CheckNextState()
    {
        StartCoroutine(IECheckNextState());
    }

    private IEnumerator IECheckNextState()
    {
        while (true)
        {
            yield return null; //1 frame
            if (!ShouldTransitionToNextState()) continue;
            TransitionToNextState();
        }
    }

    private void TransitionToNextState()
    {
        StateType nextState = GetNextState();
        NotifyToSubscribers(nextState.ToString());
    }

    [Button]
    private bool ShouldTransitionToNextState()
    {
        return HeroReference.Instance.Heroes.All(hero => !hero.Data.isActive);
    }

    protected override void NotifyToSubscribers(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnHeroStateChange,
            new object[] { value }));
    }

    private StateType GetNextState()
    {
        Enum.TryParse(currentState, out StateType item);
        var indexOfCurrentState = this.stateOrder.IndexOf(item);
        return indexOfCurrentState == this.stateOrder.Count - 1 ? stateOrder[0] : stateOrder[indexOfCurrentState + 1];
    }

    private void SetStateOrder()
    {
        this.stateOrder = new List<StateType>((StateType[])Enum.GetValues(typeof(StateType)));
    }
}