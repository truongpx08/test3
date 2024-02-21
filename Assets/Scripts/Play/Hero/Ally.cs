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
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Init.Data.currentCell.Data.cellToJumpOfAlly);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Init.Data.nextCell.Data.cellToJumpOfAlly);
    }

    protected override void Move()
    {
        if (this.Init.Data.currentCell.Data.type == CellType.ReserveAlly)
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == CellType.AllySpawnPoint && !c.HasHero);
            if (!cell) return;
            this.Movement.PlayAnimJump(cell);
            return;
        }

        this.Movement.JumpNextCell();
    }
}