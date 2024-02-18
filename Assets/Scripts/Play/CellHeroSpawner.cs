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
    public void SpawnAlly()
    {
        var go = SpawnObjectWithName(HeroName.Ally);
        SetUpGo(go);
    }

    [Button]
    public void SpawnEnemy()
    {
        var go = SpawnObjectWithName(HeroName.Enemy);
        SetUpGo(go);
    }

    private void SetUpGo(Transform go)
    {
        var hero = go.GetComponent<Hero>();
        hero.Spawn(this.cell);
        HeroReference.Instance.heroes.Add(hero);
    }
}