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
            this.pet.Data.hp = Mathf.Clamp(this.Data.hp - this.damageReceived, 0, 50);
            this.pet.HpText.UpdateText(this.pet.Data.hp.ToString());
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