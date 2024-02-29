using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetReference : TruongSingleton<PetReference>
{
    [SerializeField] private List<Pet> pets;
    public List<Pet> Pets => pets;
    [SerializeField] private Pet botBoss;
    public Pet BotBoss => botBoss;
    [SerializeField] private Pet topBoss;
    public Pet TopBoss => topBoss;

    public Pet GetBoss(string dataType)
    {
        switch (dataType)
        {
            case PetType.Bot:
                return TopBoss;
            case PetType.Top:
                return BotBoss;
        }

        return null;
    }

    public void AddTopBoss(Pet value)
    {
        this.topBoss = value;
    }

    public void AddBotBoss(Pet value)
    {
        this.botBoss = value;
    }
}