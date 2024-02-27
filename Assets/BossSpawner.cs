using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BossSpawner : SpawnerObj
{
    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName(TruongConstants.Hero);
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SpawnAlly();
        SpawnEnemy();
    }

    [Button]
    public void SpawnAlly()
    {
        var go = SpawnObjectWithName(HeroType.Ally);
        SetUpGo(go);
        go.transform.localPosition = new Vector3(0, -3.5f, 0);
        HeroReference.Instance.AddAllyBoss(go.GetComponent<Hero>());
    }

    [Button]
    public void SpawnEnemy()
    {
        var go = SpawnObjectWithName(HeroType.Enemy);
        SetUpGo(go);
        go.transform.localPosition = new Vector3(0, 3.5f, 0);
        HeroReference.Instance.AddEnemyBoss(go.GetComponent<Hero>());
    }

    private void SetUpGo(Transform go)
    {
        var hero = go.GetComponent<Hero>();
        hero.Init.Init();
        hero.Init.AddIsBoss(true);
        HeroReference.Instance.Heroes.Add(hero);
    }
}