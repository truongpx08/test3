using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Hero
{
    protected override void SetColor()
    {
        this.model.color = Color.green;
    }

    protected override Cell GetCellToJump()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.currentCell.Data.cellToJumpOfAlly);
    }

    protected override Cell GetNextCellToJump()
    {
        return PlayObjects.Instance.CellSpawner.GetCellWithId(this.data.cellToJump.Data.cellToJumpOfAlly);
    }
}