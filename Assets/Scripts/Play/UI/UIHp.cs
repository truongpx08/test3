using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHp : UIText
{
    protected override void OnHpChange(int value)
    {
        base.OnHpChange(value);
        SetText(value.ToString());
    }
}
