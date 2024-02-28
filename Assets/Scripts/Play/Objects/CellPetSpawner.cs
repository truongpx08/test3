using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class CellPetSpawner : TruongSpawner
{
    private Cell cell;
    [SerializeField] private int count;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        count = 0;
    }

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

    [Button]
    public void SpawnAlly()
    {
        var go = SpawnObjectWithName(PetType.Ally);
        SetUpGo(go);
    }

    [Button]
    public void SpawnEnemy()
    {
        var go = SpawnObjectWithName(PetType.Enemy);
        SetUpGo(go);
    }

    private void SetUpGo(Transform go)
    {
        var hero = go.GetComponent<Pet>();
        hero.Init.AddCurrentCell(cell);
        hero.Init.Init();
        hero.Init.AddId(count);
        PetReference.Instance.Heroes.Add(hero);
        this.count++;
    }
}