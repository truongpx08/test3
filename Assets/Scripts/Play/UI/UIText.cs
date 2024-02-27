using System.Collections;
using TMPro;
using System.Collections.Generic;
using UnityEngine;

public class UIText : PlaySubscriber
{
    [SerializeField] protected TextMeshProUGUI textMesh;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextMesh();
    }

    private void LoadTextMesh()
    {
        this.textMesh = GetComponent<TextMeshProUGUI>();
    }

    protected void SetText(string value)
    {
        textMesh.text = value;
    }
}