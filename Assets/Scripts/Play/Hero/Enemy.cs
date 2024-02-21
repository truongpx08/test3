using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Hero
{
    public override void AddName()
    {
        this.Init.SetName(HeroName.Enemy);
    }

    public override void AddColor()
    {
        this.Init.SetColor(Color.red);
    }

    public override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Init.Data.currentCell.Data.cellToJumpOfEnemy);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Init.Data.nextCell.Data.cellToJumpOfEnemy);
    }

    protected override void Move()
    {
        if (this.Init.Data.currentCell.Data.type == CellType.ReserveEnemy)
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == CellType.EnemySpawnPoint && !c.HasHero);
            if (!cell) return;
            this.Movement.PlayAnimJump(cell);
            return;
        }

        this.Movement.JumpNextCell();
    }
}