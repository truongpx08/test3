using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroAttacker : HeroRefAbstract
{
    public void Attack()
    {
        this.data.nextCell = this.hero.GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.HasHero)
        {
            PlayAnimAttack(this.data.nextCell.Hero);
            return;
        }

        this.data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (!this.data.subsequentCell.HasHero) return;

        PlayAnimAttack(this.data.subsequentCell.Hero);
    }

    private void PlayAnimAttack(Hero target)
    {
        Debug.Log("Attack ");
        this.hero.Init.SetIsInStatus(true);
        DOVirtual.DelayedCall(data.durationAnim, () =>
        {
            target.Attacker.Hurt(this.data.atk);
            this.hero.Init.SetIsInStatus(false);
        });
    }

    private void Hurt(int value)
    {
        Debug.Log("Hurt " + value);
        this.hero.HpText.UpdateText(Mathf.Clamp(this.data.hp - value, 0, 50).ToString());
        if (this.data.hp <= 0) Died();
    }

    private void Died()
    {
        DOVirtual.DelayedCall(0.1f, () => { this.data.currentCell.HeroDespawner.DespawnObject(this.hero.transform); });
    }
}