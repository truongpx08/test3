using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroMovement : HeroRefAbstract
{
    public void TryMove()
    {
        if (this.hero.Init.Data.currentCell.Data.type == this.hero.GetReserveCellType())
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == this.hero.GetSpawnPointCellType() && !c.HasHero);
            if (!cell) return;
            Move(cell);
            return;
        }

        this.data.nextCell = this.hero.GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.Data.type == CellType.ReserveEnemy) return;
        if (this.data.nextCell.Data.type == CellType.ReserveAlly) return;
        if (this.data.nextCell.HasHero) return;

        this.data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (this.data.subsequentCell.HasHero) return;

        Move(this.data.nextCell);
    }

    private void Move(Cell nextCell)
    {
        this.hero.Action.CallAction(() =>
        {
            this.hero.transform.DOMove(nextCell.gameObject.transform.position, data.durationAnim)
                .OnComplete(() =>
                {
                    var thisTransform = this.hero.transform;
                    thisTransform.parent = nextCell.HeroSpawner.Holder.transform;
                    this.data.currentCell.HeroSpawner.Holder.Items.Clear();
                    nextCell.HeroSpawner.Holder.Items.Add(thisTransform);
                    this.hero.Init.AddCurrentCell(nextCell);
                    this.hero.Init.SetIsActive(false);
                });
        });
    }
}