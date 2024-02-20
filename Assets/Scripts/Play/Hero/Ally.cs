using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Hero
{
    protected override void AddName()
    {
        SetName(HeroName.Ally);
    }


    protected override void AddColor()
    {
        this.model.color = Color.blue;
    }

    protected override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.currentCell.Data.cellToJumpOfAlly);
    }

    protected override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.nextCell.Data.cellToJumpOfAlly);
    }

    protected override void Move()
    {
        if (this.data.currentCell.Data.type == CellType.ReserveAlly)
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == CellType.AllySpawnPoint && !c.HasHero);
            if (!cell) return;
            PlayAnimJump(cell);
            return;
        }

        JumpNextCell();
    }
}