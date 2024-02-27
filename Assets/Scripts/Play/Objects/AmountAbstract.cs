using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AmountAbstract : PlaySubscriber
{
    [SerializeField] protected int amount;
    [SerializeField] protected int min;
    [SerializeField] protected int max;
    private string messageType;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        AddMaxAmount();
        AddMinAmount();
        AddMessageType();
    }

    protected abstract void AddMaxAmount();

    protected void SetMaxAmount(int value)
    {
        this.max = value;
    }

    protected abstract void AddMinAmount();

    protected void SetMinAmount(int value)
    {
        this.min = value;
    }

    protected override void OnGameStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SetAmountOnStart();
    }

    protected abstract void SetAmountOnStart();

    protected void SetAmount(int value)
    {
        this.amount = value;
    }

    protected abstract void AddMessageType();

    protected void SetMessageType(string onHpChange)
    {
        this.messageType = onHpChange;
    }

    protected void NotifyToSubscribers(int value)
    {
        TruongObserver.Instance.Notify(this.messageType, new object[] { value });
    }
}