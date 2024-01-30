using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Hero
{
    protected override void SetColor()
    {
        this.model.color = Color.red;
    }

    protected override Cell GetCellToJump()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.currentCell.Data.cellToJumpOfEnemy);
    }

    protected override Cell GetNextCellToJump()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.cellToJump.Data.cellToJumpOfEnemy);
    }
}