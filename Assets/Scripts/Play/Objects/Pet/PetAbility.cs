using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PetAbility : PetAction
{
    [Serializable] public class AbilityData
    {
        public int id;
        public PetState.StateType triggerState;
        public string description;
    }

    [SerializeField] private AbilityData data;

    public void TryUse(PetState.StateType petState, Pet petRef)
    {
        if (!CanUse(petState)) return;
        AddPetRef(petRef);
        if (!petRef.Data.canMove) return;
        Use();
    }

    private void Use()
    {
        CallActionWithDelay(() =>
        {
            this.pet.Hp.ChangeValue(this.PetData.hp + 1);
            Debug.Log("Ability");
        });
    }

    private bool CanUse(PetState.StateType petState)
    {
        return this.data.triggerState == petState;
    }
}