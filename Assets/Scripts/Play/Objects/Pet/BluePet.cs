using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BluePet : Pet
{
    public override void AddType()
    {
        this.Init.SetType(PetType.Enemy);
    }

    public override void AddColor()
    {
        this.Init.SetColor(Color.red);
    }

    public override void AddFinishCellType()
    {
        SetFinishCellType(CellType.EnemyFinish);
    }

    public override Cell GetNextCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.enemyNextCell);
    }

    public override Cell GetSubsequentCell()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.enemyNextCell);
    }

    public override string GetReserveType()
    {
        return CellType.ReserveEnemy;
    }
}