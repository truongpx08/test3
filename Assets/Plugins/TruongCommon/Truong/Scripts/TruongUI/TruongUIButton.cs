using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class TruongUIButton : TruongUIText
{
    protected void AddActionToButton(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}