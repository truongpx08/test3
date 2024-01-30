using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class CellHeroSpawner : TruongSpawner
{
    private Cell cell;

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
        LoadPrefabInResourceWithPrefabName("Hero");
    }

    [Button]
    private void SpawnAlly()
    {
        var go = SpawnObjectWithName("Ally");
        SetUpGo(go);
    }

    [Button]
    private void SpawnEnemy()
    {
        var go = SpawnObjectWithName("Enemy");
        SetUpGo(go);
    }

    private void SetUpGo(Transform go)
    {
        go.GetComponent<Hero>().Spawn(this.cell);
    }
}