using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPet : Pet
{
    public override void AddType()
    {
        this.Init.SetType(PetType.Ally);
    }

    public override void AddColor()
    {
        this.Init.SetColor(Color.blue);
    }

    public override void AddFinishCellType()
    {
        SetFinishCellType(CellType.AllyFinish);
    }


    public override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.allyNextCell);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.allyNextCell);
    }

    public override string GetReserveType()
    {
        return CellType.ReserveAlly;
    }
}