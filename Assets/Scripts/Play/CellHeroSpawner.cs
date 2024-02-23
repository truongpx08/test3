using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class CellHeroSpawner : TruongSpawner
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
        LoadPrefabInResourceWithPrefabName("Hero");
    }

    [Button]
    public void SpawnAlly()
    {
        var go = SpawnObjectWithName(HeroType.Ally);
        SetUpGo(go);
    }

    [Button]
    public void SpawnEnemy()
    {
        var go = SpawnObjectWithName(HeroType.Enemy);
        SetUpGo(go);
    }

    private void SetUpGo(Transform go)
    {
        var hero = go.GetComponent<Hero>();
        hero.Init.AddCurrentCell(cell);
        hero.Init.Init();
        hero.Init.AddId(count);
        HeroReference.Instance.heroes.Add(hero);
        this.count++;
    }
}