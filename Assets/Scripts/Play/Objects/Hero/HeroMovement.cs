using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class HeroMovement : HeroAction
{
    public void TryMove()
    {
        if (!this.Data.canMove) return;
        Move(this.Data.nextCell);
    }

    private void Move(Cell nextCell)
    {
        CallAction(() =>
        {
            this.hero.transform.DOMove(nextCell.gameObject.transform.position, Data.durationAnim)
                .OnComplete(() =>
                {
                    this.Data.currentCell.HeroSpawner.Holder.Items.Remove(this.hero.transform);
                    nextCell.HeroSpawner.Holder.AddItem(this.hero.transform);

                    this.hero.Init.AddCurrentCell(nextCell);
                    SetCanMove(false);
                    this.hero.Init.SetIsActive(false);
                });
        });
    }

    public void SetCanMove(bool value)
    {
        this.Data.canMove = value;
    }

    public bool GetCanMove()
    {
        if (this.Data.currentCell.Data.type == hero.Data.finishCellType) return false;

        this.Data.nextCell = this.hero.GetNextCell();
        if (this.Data.nextCell == null) return false;
        if (this.Data.nextCell.HeroSpawner.Holder.Items.Any(h => h.gameObject.activeSelf))
        {
            if (hero.IsTeammate(this.Data.nextCell.Hero))
            {
                if (!Data.nextCell.Hero.Data.canMove)
                    return false;
                return true;
            }

            return false;
        }

        this.Data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return false;
        if (this.Data.subsequentCell.HasHero)
        {
            if (hero.IsOpponent(this.Data.subsequentCell.Hero)) return false;
        }

        return true;
    }
}