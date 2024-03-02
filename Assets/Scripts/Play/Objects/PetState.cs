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
        AfterMove = 3,
        Attack = 4,
        Injury = 5,
        Faint = 6,
        AfterFaint = 7,
    }

    protected override void OnPetStateChange(PetState.StateType state)
    {
        base.OnPetStateChange(state);
        SetCurrentState(state.ToString());
        // Debug.Log("SetCurrentState: " + state);
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
            if (!CanTransitionToNextState()) continue;
            TransitionToNextState();
        }
    }

    private void TransitionToNextState()
    {
        StateType nextState = GetNextState();
        NotifyToSubscribers(nextState.ToString());
    }

    [Button]
    private bool CanTransitionToNextState()
    {
        if (PetReference.Instance.Pets.Any(item => item.Data.isActive)) return false;
        if (PetReference.Instance.BotBoss.Data.isActive) return false;
        if (PetReference.Instance.TopBoss.Data.isActive) return false;
        return true;
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