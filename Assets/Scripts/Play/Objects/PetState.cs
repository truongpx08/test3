using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PetState : State
{
    [SerializeField] private List<StateType> stateOrder;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        AddStateOrder();
    }

    public enum StateType
    {
        None = 0,
        BeforeMove = 1,
        Move = 2,
        Attack = 3,
        Injury = 4,
        Faint = 5,
        AfterFaint = 6,
    }

    protected override void OnPetStateChange(PetState.StateType value)
    {
        base.OnPetStateChange(value);
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
        return PetReference.Instance.Pets.All(item => !item.Data.isActive);
    }

    protected override void NotifyToSubscribers(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnPetStateChange,
            new object[] { value }));
    }

    private StateType GetNextState()
    {
        Enum.TryParse(currentState, out StateType item);
        var indexOfCurrentState = this.stateOrder.IndexOf(item);
        return indexOfCurrentState == this.stateOrder.Count - 1 ? stateOrder[0] : stateOrder[indexOfCurrentState + 1];
    }

    private void AddStateOrder()
    {
        this.stateOrder = new List<StateType>((StateType[])Enum.GetValues(typeof(StateType)));
    }
}