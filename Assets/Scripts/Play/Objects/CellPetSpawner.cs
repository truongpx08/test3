using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class CellPetSpawner : TruongSpawner
{
    [SerializeField] private Cell cell;
    public Cell Cell => cell;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCell();
    }

    private void LoadCell()
    {
        this.cell = GetComponentInParent<Cell>();
    }

    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName(TruongConstants.Pet);
    }

    public Pet SpawnPet()
    {
        return SpawnDefaultObject().GetComponent<Pet>();
    }
}