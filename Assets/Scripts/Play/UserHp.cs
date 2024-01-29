using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHp : AmountAbstract
{
    protected override void SetMaxAmount()
    {
        SetMaxAmount(10);
    }

    protected override void SetMinAmount()
    {
        SetMinAmount(0);
    }

    protected override void OnAmountChange(int value)
    {
        TruongObserver.Instance.Notify(MessageType.OnHpChange, new object[] { value });
    }

    protected override void SetAmountOnStart()
    {
        SetAmount(this.max);
    }
}