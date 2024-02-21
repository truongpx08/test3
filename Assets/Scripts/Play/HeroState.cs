using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.TestTools;

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
        // Appear,
        Movement,
        Attack,
        Injury
        // Disappear,
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
            yield return new WaitForSeconds(0.1f);
            if (!ShouldTransitionToNextState()) continue;
            TransitionToNextState();
        }
    }

    private void TransitionToNextState()
    {
        StateType nextState = GetNextState();
        SendStateToSubscribers(nextState.ToString());
    }

    [Button]
    private bool ShouldTransitionToNextState()
    {
        return HeroReference.Instance.heroes.All(hero => !hero.Init.Data.isActive);
    }

    protected override void SendStateToSubscribers(string value)
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