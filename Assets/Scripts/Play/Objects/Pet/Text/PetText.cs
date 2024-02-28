using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PetText : TMPTextHandler
{
    [SerializeField] protected Pet pet;

    private void LoadRef()
    {
        this.pet = GetComponentInParent<Pet>();
    }

    public override void UpdateText(string value)
    {
        base.UpdateText(value);
        LoadRef();
        SaveData();
    }

    protected abstract void SaveData();

}