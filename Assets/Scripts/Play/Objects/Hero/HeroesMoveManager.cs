using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroesMoveManager : PlaySubscriber
{
    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        if (value != HeroState.StateType.BeforeMove) return;
        CalculateMoveAllies();
        CalculateMoveEnemies();
    }

    private void CalculateMoveEnemies()
    {
        List<Hero> heroList = GetEnemies();
        heroList = heroList.OrderBy(item => item.Data.currentCell.Data.enemyPathId).ToList();
        CalculateMove(heroList);
    }

    private void CalculateMoveAllies()
    {
        List<Hero> heroList = GetAllies();
        heroList = heroList.OrderByDescending(item => item.Data.currentCell.Data.allyPathId).ToList();
        CalculateMove(heroList);
    }

    private void CalculateMove(List<Hero> heroList)
    {
        heroList.ForEach(hero =>
        {
            if (!hero.Movement.GetCanMove())
            {
                hero.Movement.SetCanMove(false);
                return;
            }

            hero.Movement.SetCanMove(true);
        });
    }

    private List<Hero> GetAllies()
    {
        return GetHeroListWithType(HeroType.Ally);
    }


    private List<Hero> GetEnemies()
    {
        return GetHeroListWithType(HeroType.Enemy);
    }

    private List<Hero> GetHeroListWithType(string heroType)
    {
        List<Hero> heroes = new List<Hero>();

        HeroReference.Instance.Heroes.ForEach(hero =>
        {
            if (hero.Data.type != heroType) return;
            if (hero.Data.isBoss) return;
            heroes.Add(hero);
        });
        return heroes;
    }
}