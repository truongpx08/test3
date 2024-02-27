using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHp : AmountAbstract
{
    protected override void OnHpChange(int value)
    {
        base.OnHpChange(value);
        SetAmount(value);
    }

    protected override void AddMaxAmount()
    {
        SetMaxAmount(10);
    }

    protected override void AddMinAmount()
    {
        SetMinAmount(0);
    }

    protected override void AddMessageType()
    {
        SetMessageType(MessageType.OnHpChange);
    }

    protected override void SetAmountOnStart()
    {
        NotifyToSubscribers(this.max);
    }
}