using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hero : GameObj
{
    [SerializeField] private Tile atTile;

    public void SetAtTile(Tile value)
    {
        this.atTile = value;
    }

    protected override void OnTimeChange(int value)
    {
        Jump();
    }

    private void Jump()
    {
        var target = PlayObjects.Instance.CellSpawner.GetNextTile(atTile);
        if (target == null) return;
        this.transform.DOMove(target.gameObject.transform.position, 0.25f).OnComplete(() => SetAtTile(target));
    }

    protected override void OnStateChange(string value)
    {
    }
}