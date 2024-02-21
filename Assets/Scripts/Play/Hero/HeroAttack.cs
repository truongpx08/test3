using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroAttack : HeroRefAbstract
{
    public void TryAttack()
    {
        this.data.nextCell = this.hero.GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.HasHero)
        {
            Attack(this.data.nextCell.Hero);
            return;
        }

        this.data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (!this.data.subsequentCell.HasHero) return;

        Attack(this.data.subsequentCell.Hero);
    }

    private void Attack(Hero target)
    {
        this.hero.Action.CallActionWithDelay(() =>
        {
            target.Injury.SetWasAttacked(true);
            target.Injury.SetDamageReceived(this.data.atk);
        });
    }
}