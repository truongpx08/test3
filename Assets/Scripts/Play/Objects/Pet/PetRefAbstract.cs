using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PetRefAbstract : TruongMonoBehaviour
{
    [SerializeField] protected Pet pet;
    protected PetData PetData => this.pet.Data;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPetRef();
    }

    private void LoadPetRef()
    {
        AddPetRef(GetComponentInParent<Pet>());
    }

    protected void AddPetRef(Pet value)
    {
        if (this.pet != null) return;
        this.pet = value;
    }

    [Button]
    protected void ReloadPetRef()
    {
        if (this.pet != null) return;
        LoadPetRef();
    }
}