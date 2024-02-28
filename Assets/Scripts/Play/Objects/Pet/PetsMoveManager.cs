using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetsMoveManager : PlaySubscriber
{
    protected override void OnPetStateChange(PetState.StateType value)
    {
        base.OnPetStateChange(value);
        if (value != PetState.StateType.BeforeMove) return;
        CalculateMoveAllies();
        CalculateMoveEnemies();
    }

    private void CalculateMoveEnemies()
    {
        List<Pet> heroList = GetEnemies();
        heroList = heroList.OrderBy(item => item.Data.currentCell.Data.enemyPathId).ToList();
        CalculateMove(heroList);
    }

    private void CalculateMoveAllies()
    {
        List<Pet> heroList = GetAllies();
        heroList = heroList.OrderByDescending(item => item.Data.currentCell.Data.allyPathId).ToList();
        CalculateMove(heroList);
    }

    private void CalculateMove(List<Pet> heroList)
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

    private List<Pet> GetAllies()
    {
        return GetHeroListWithType(PetType.Ally);
    }


    private List<Pet> GetEnemies()
    {
        return GetHeroListWithType(PetType.Enemy);
    }

    private List<Pet> GetHeroListWithType(string heroType)
    {
        List<Pet> heroes = new List<Pet>();

        PetReference.Instance.Heroes.ForEach(hero =>
        {
            if (hero.Data.type != heroType) return;
            if (hero.Data.isBoss) return;
            heroes.Add(hero);
        });
        return heroes;
    }
}