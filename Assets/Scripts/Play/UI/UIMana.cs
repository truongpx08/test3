using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMana : UIText
{
    protected override void OnManaChange(int value)
    {
        base.OnManaChange(value);
        SetText(value.ToString());
    }
}
