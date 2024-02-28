using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetRefAbstract : TruongMonoBehaviour
{
    [SerializeField] protected Pet pet;
    protected PetData Data => this.pet.Data;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHeroRef();
    }

    private void LoadHeroRef()
    {
        this.pet = GetComponentInParent<Pet>();
    }
}