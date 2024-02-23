using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Hero
{
    public override void AddType()
    {
        this.Init.SetType(HeroType.Ally);
    }

    public override void AddColor()
    {
        this.Init.SetColor(Color.blue);
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