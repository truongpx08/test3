using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class TruongUIText : TruongUI
{
    protected void SetTextMeshProUGUI(TextMeshProUGUI textMeshProUGUI, string value)
    {
        textMeshProUGUI.text = value;
    }
}