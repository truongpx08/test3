using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroInjury : HeroRefAbstract
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
        this.hero.HpText.UpdateText(Mathf.Clamp(this.data.hp - this.damageReceived, 0, 50).ToString());
        this.wasAttacked = false;
        if (this.data.hp <= 0) Died();
    }

    private void Died()
    {
        DOVirtual.DelayedCall(0.1f, () => { this.data.currentCell.HeroDespawner.DespawnObject(this.hero.transform); });
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