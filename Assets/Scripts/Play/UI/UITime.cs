using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITime : UIText
{
    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        SetText(value.ToString());
    }
}