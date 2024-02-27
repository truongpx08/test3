using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMana : AmountAbstract
{
    protected override void OnManaChange(int value)
    {
        base.OnManaChange(value);
        SetAmount(value);
    }

    protected override void AddMessageType()
    {
        SetMessageType(MessageType.OnManaChange);
    }

    protected override void SetAmountOnStart()
    {
        NotifyToSubscribers(this.min);
    }

    protected override void AddMaxAmount()
    {
        SetMaxAmount(8);
    }

    protected override void AddMinAmount()
    {
        SetMinAmount(0);
    }

    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        if (this.amount >= this.max) return;
        NotifyToSubscribers(this.amount + 1);
    }
}