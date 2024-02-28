using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetReference : TruongSingleton<PetReference>
{
    [SerializeField] private List<Pet> heroes;
    public List<Pet> Heroes => heroes;
    [SerializeField] private Pet allyBoss;
    public Pet AllyBoss => allyBoss;
    [SerializeField] private Pet allyEnemy;
    public Pet AllyEnemy => allyEnemy;

    public Pet GetBoss(string dataType)
    {
        switch (dataType)
        {
            case PetType.Ally:
                return AllyEnemy;
            case PetType.Enemy:
                return AllyBoss;
        }

        return null;
    }

    public void AddEnemyBoss(Pet value)
    {
        this.allyEnemy = value;
    }

    public void AddAllyBoss(Pet value)
    {
        this.allyBoss = value;
    }
}