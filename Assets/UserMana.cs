using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMana : AmountAbstract
{
    protected override void OnAmountChange(int value)
    {
        TruongObserver.Instance.Notify(MessageType.OnManaChange, new object[] { value });
    }

    protected override void SetAmountOnStart()
    {
        SetAmount(this.min);
    }

    protected override void SetMaxAmount()
    {
        SetMaxAmount(8);
    }

    protected override void SetMinAmount()
    {
        SetMinAmount(0);
    }

    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        if (this.amount >= this.max) return;
        SetAmount(this.amount + 1);
    }
}