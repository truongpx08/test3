using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hero : GameObj
{
    [SerializeField] private Cell atCell;

    public void SetAtTile(Cell value)
    {
        this.atCell = value;
    }

    protected override void OnTimeChange(int value)
    {
        Jump();
    }

    private void Jump()
    {
        var target = PlayObjects.Instance.CellSpawner.GetTileToJump(atCell);
        if (target == null) return;
        this.transform.DOMove(target.gameObject.transform.position, 0.25f).OnComplete(() => SetAtTile(target));
    }

    protected override void OnStateChange(string value)
    {
    }
}