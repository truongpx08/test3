using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData : TruongSingleton<PlayData>
{
    [SerializeField] private PetsData petsData;
    public PetsData PetsData => petsData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPetsData();
    }

    private void LoadPetsData()
    {
        this.petsData = Resources.Load<PetsData>("ScriptableObjects/PetsData");
    }
}