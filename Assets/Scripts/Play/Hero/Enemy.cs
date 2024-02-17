using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Hero
{
    protected override void AddName()
    {
        SetName(HeroName.Enemy);
    }

    protected override void AddColor()
    {
        SetColor(Color.red);
    }

    protected override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.currentCell.Data.cellToJumpOfEnemy);
    }

    protected override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.nextCell.Data.cellToJumpOfEnemy);
    }

    protected override void Move()
    {
        if (this.data.currentCell.Data.type == CellType.ReserveEnemy)
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == CellType.EnemySpawnPoint && !c.HasHero);
            if (!cell) return;
            PlayAnimJump(cell);
            return;
        }

        JumpNextCell();
    }
}