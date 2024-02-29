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
        SpawnBotBoss();
        SpawnTopBoss();
    }

    [Button]
    public void SpawnBotBoss()
    {
        var pet = SummonPet(PetType.Bot);

        pet.transform.localPosition = new Vector3(0, -3.5f, 0);
        PetReference.Instance.AddBotBoss(pet);
    }

    [Button]
    public void SpawnTopBoss()
    {
        var pet = SummonPet(PetType.Top);

        pet.transform.localPosition = new Vector3(0, 3.5f, 0);
        PetReference.Instance.AddTopBoss(pet);
    }


    private Pet SummonPet(string type)
    {
        var pet = SpawnDefaultObject().GetComponent<Pet>();

        pet.AddDataWithPetId(PetData.BossId);
        pet.Init.SetType(type);
        pet.Init.Init();
        pet.Init.AddIsBoss(true);
        return pet;
    }
}