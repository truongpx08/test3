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
        return PlayObjects.Instance.CellSpawner.GetCellToJumpOfEnemy(atCell);
    }
}