using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class TMPTextHandler : TruongMonoBehaviour
{
    [SerializeField] protected TextMeshPro textMeshPro;
    [SerializeField] protected string text;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTMP();
    }

    private void LoadTMP()
    {
        this.textMeshPro = GetComponent<TextMeshPro>();
    }

    [Button]
    public virtual void UpdateText(string value)
    {
        this.text = value;
        this.textMeshPro.text = text;
    }
}