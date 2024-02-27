using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroReference : TruongSingleton<HeroReference>
{
    [SerializeField] private List<Hero> heroes;
    public List<Hero> Heroes => heroes;
    [SerializeField] private Hero allyBoss;
    public Hero AllyBoss => allyBoss;
    [SerializeField] private Hero allyEnemy;
    public Hero AllyEnemy => allyEnemy;

    public Hero GetBoss(string dataType)
    {
        switch (dataType)
        {
            case HeroType.Ally:
                return AllyEnemy;
            case HeroType.Enemy:
                return AllyBoss;
        }

        return null;
    }

    public void AddEnemyBoss(Hero value)
    {
        this.allyEnemy = value;
    }

    public void AddAllyBoss(Hero value)
    {
        this.allyBoss = value;
    }
}