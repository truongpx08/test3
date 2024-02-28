using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BossSpawner : SpawnerObj
{
    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName(TruongConstants.Pet);
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SpawnPlayerBoss();
        SpawnBotBoss();
    }

    [Button]
    public void SpawnPlayerBoss()
    {
        var go = SpawnObjectWithName(PetType.Ally);
        SetUpGo(go);
        go.transform.localPosition = new Vector3(0, -3.5f, 0);
        PetReference.Instance.AddAllyBoss(go.GetComponent<Pet>());
    }

    [Button]
    public void SpawnBotBoss()
    {
        var go = SpawnObjectWithName(PetType.Enemy);
        SetUpGo(go);
        go.transform.localPosition = new Vector3(0, 3.5f, 0);
        PetReference.Instance.AddEnemyBoss(go.GetComponent<Pet>());
    }

    private void SetUpGo(Transform go)
    {
        var hero = go.GetComponent<Pet>();
        hero.Init.Init();
        hero.Init.AddIsBoss(true);
        PetReference.Instance.Heroes.Add(hero);
    }
}