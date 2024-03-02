using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PetInjury : PetAction
{
    [SerializeField] private bool wasAttacked;
    [SerializeField] private int damageReceived;

    public void TryHurt()
    {
        if (!wasAttacked) return;
        Hurt();
    }

    private void Hurt()
    {
        CallActionWithDelay(() =>
        {
            this.pet.Hp.ChangeValue(this.PetData.hp - this.damageReceived);
            this.wasAttacked = false;
        });
    }

    public void SetWasAttacked(bool value)
    {
        this.wasAttacked = value;
    }

    public void SetDamageReceived(int value)
    {
        this.damageReceived = value;
    }
}