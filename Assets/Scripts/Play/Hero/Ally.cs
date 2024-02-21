using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Hero
{
    public override void AddName()
    {
        this.Init.SetName(HeroName.Ally);
    }

    public override void AddColor()
    {
        this.Init.SetColor(Color.blue);
    }

    public override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.cellToJumpOfAlly);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.cellToJumpOfAlly);
    }

    public override string GetReserveCellType()
    {
        return CellType.ReserveAlly;
    }

    public override string GetSpawnPointCellType()
    {
        return CellType.AllySpawnPoint;
    }
}