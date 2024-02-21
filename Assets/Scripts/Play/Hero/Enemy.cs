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
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.cellToJumpOfEnemy);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.cellToJumpOfEnemy);
    }

    public override string GetReserveCellType()
    {
        return CellType.ReserveEnemy;
    }

    public override string GetSpawnPointCellType()
    {
        return CellType.EnemySpawnPoint;
    }
}