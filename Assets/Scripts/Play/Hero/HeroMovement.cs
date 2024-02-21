using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroMovement : HeroRefAbstract
{
    public void JumpNextCell()
    {
        this.data.nextCell = this.hero.GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.Data.type == CellType.ReserveEnemy) return;
        if (this.data.nextCell.Data.type == CellType.ReserveAlly) return;
        if (this.data.nextCell.HasHero) return;

        this.data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (this.data.subsequentCell.HasHero) return;

        PlayAnimJump(this.data.nextCell);
    }

    public void PlayAnimJump(Cell nextCell)
    {
        this.hero.Init.SetIsInStatus(true);
        this.hero.transform.DOMove(nextCell.gameObject.transform.position, data.durationAnim)
            .OnComplete(() =>
            {
                var thisTransform = this.hero.transform;
                thisTransform.parent = nextCell.HeroSpawner.Holder.transform;
                this.data.currentCell.HeroSpawner.Holder.Items.Clear();
                nextCell.HeroSpawner.Holder.Items.Add(thisTransform);
                this.hero.Init.AddCurrentCell(nextCell);
                this.hero.Init.SetIsInStatus(false);
            });
    }
}