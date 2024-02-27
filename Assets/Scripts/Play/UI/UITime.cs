using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITime : PlaySubscriber
{
    [SerializeField] private TextMeshProUGUI textMesh;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextMesh();
    }

    private void LoadTextMesh()
    {
        this.textMesh = GetComponent<TextMeshProUGUI>();
    }

    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        textMesh.text = value.ToString();
    }
}