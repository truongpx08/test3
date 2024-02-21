using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroMovement : HeroAction
{
    public void TryMove()
    {
        if (this.hero.Data.currentCell.Data.type == this.hero.GetReserveCellType())
        {
            var cell = PlayObjects.Instance.CellSpawner.Cells.Find(c =>
                c.Data.type == this.hero.GetSpawnPointCellType() && !c.HasHero);
            if (!cell) return;
            Move(cell);
            return;
        }

        this.Data.nextCell = this.hero.GetNextCell();
        if (this.Data.nextCell == null) return;
        if (this.Data.nextCell.Data.type == CellType.ReserveEnemy) return;
        if (this.Data.nextCell.Data.type == CellType.ReserveAlly) return;
        if (this.Data.nextCell.HasHero) return;

        this.Data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return;
        if (this.Data.subsequentCell.HasHero)
        {
            if (!HasAllyAtCell(this.Data.subsequentCell))
                return;
        }

        Move(this.Data.nextCell);
    }

    private void Move(Cell nextCell)
    {
        CallAction(() =>
        {
            this.hero.transform.DOMove(nextCell.gameObject.transform.position, Data.durationAnim)
                .OnComplete(() =>
                {
                    this.Data.currentCell.HeroSpawner.Holder.Items.Clear();
                    nextCell.HeroSpawner.Holder.AddItem(this.hero.transform);

                    this.hero.Init.AddCurrentCell(nextCell);
                    this.hero.Init.SetIsActive(false);
                });
        });
    }
}