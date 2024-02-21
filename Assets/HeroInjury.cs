using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroInjury : HeroAction
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
            this.hero.HpText.UpdateText(Mathf.Clamp(this.Data.hp - this.damageReceived, 0, 50).ToString());
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