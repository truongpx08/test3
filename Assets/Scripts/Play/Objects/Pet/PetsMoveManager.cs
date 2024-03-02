using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PetsMoveManager : PlaySubscriber
{
    protected override void OnPetStateChange(PetState.StateType state)
    {
        base.OnPetStateChange(state);
        if (state != PetState.StateType.BeforeMove) return;
        CalculateMovementOfBotPet();
        CalculateMovementOfTopPet();
    }

    private void CalculateMovementOfTopPet()
    {
        List<Pet> list = GetTopPet();
        list = list.OrderBy(item => item.Data.currentCell.Data.topPathId).ToList();
        CalculateMove(list);
    }

    private void CalculateMovementOfBotPet()
    {
        List<Pet> list = GetBotPet();
        list = list.OrderByDescending(item => item.Data.currentCell.Data.botPathId).ToList();
        CalculateMove(list);
    }

    private void CalculateMove(List<Pet> petList)
    {
        petList.ForEach(pet =>
        {
            if (!pet.Movement.GetCanMove())
            {
                pet.Movement.SetCanMove(false);
                return;
            }

            pet.Movement.SetCanMove(true);
        });
    }

    private List<Pet> GetBotPet()
    {
        return GetPetListWithType(PetType.Top);
    }


    private List<Pet> GetTopPet()
    {
        return GetPetListWithType(PetType.Bot);
    }

    private List<Pet> GetPetListWithType(string petType)
    {
        List<Pet> pets = new List<Pet>();

        PetReference.Instance.Pets.ForEach(pet =>
        {
            if (pet.Data.type != petType) return;
            if (pet.Data.isBoss) return;
            pets.Add(pet);
        });
        return pets;
    }
}