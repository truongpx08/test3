using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmountAbstract : PlaySubscriber
{
    [SerializeField] protected int amount;
    [SerializeField] protected int min;
    [SerializeField] protected int max;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        SetMaxAmount();
        SetMinAmount();
    }

    protected abstract void SetMaxAmount();
    protected abstract void SetMinAmount();

    protected override void OnTimeChange(int value)
    {
    }

    protected override void OnGameStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SetAmountOnStart();
    }


    protected void SetAmount(int value)
    {
        this.amount = value;
        OnAmountChange(value);
    }

    protected abstract void OnAmountChange(int value);
    protected abstract void SetAmountOnStart();

    protected void SetMaxAmount(int value)
    {
        this.max = value;
    }

    protected void SetMinAmount(int value)
    {
        this.min = value;
    }
}