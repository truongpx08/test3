using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroAttack : HeroAction
{
    public void TryAttack()
    {
        this.Data.nextCell = this.hero.GetNextCell();
        if (this.Data.nextCell == null) return;
        if (this.Data.nextCell.HasHero)
        {
            Attack(this.Data.nextCell.Hero);
            return;
        }

        this.Data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return;
        if (!this.Data.subsequentCell.HasHero) return;

        Attack(this.Data.subsequentCell.Hero);
    }

    private void Attack(Hero target)
    {
        CallActionWithDelay(() =>
        {
            target.Injury.SetWasAttacked(true);
            target.Injury.SetDamageReceived(this.Data.atk);
        });
    }
}