using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class HeroAttack : HeroAction
{
    public void TryAttack()
    {
        if (this.Data.isBoss) return;
        if (this.Data.currentCell.Data.type == hero.Data.finishCellType)
        {
            var boss = HeroReference.Instance.GetBoss(this.Data.type);
            if (boss.Data.hp <= 0) return;
            Attack(boss);
            return;
        }

        this.Data.nextCell = this.hero.GetNextCell();

        if (this.Data.nextCell == null) return;
        if (HasAllyAtCell(this.Data.nextCell)) return;
        if (HasEnemyAtCell(this.Data.nextCell))
        {
            Attack(this.Data.nextCell.Hero);
            return;
        }


        this.Data.subsequentCell = this.hero.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return;
        if (!HasEnemyAtCell(this.Data.subsequentCell)) return;

        Attack(this.Data.subsequentCell.Hero);
    }


    private void Attack(Hero target)
    {
        CallAction(() =>
        {
            var bullet = this.hero.BulletSpawner.SpawnDefaultObject();
            bullet.transform.position = this.hero.transform.position;
            bullet.transform.DOMove(target.gameObject.transform.position, this.Data.durationAnim)
                .OnComplete(() =>
                {
                    target.Injury.SetWasAttacked(true);
                    target.Injury.SetDamageReceived(this.Data.atk);
                    this.hero.BulletDespawner.DespawnDefaultObject();
                    this.hero.Init.SetIsActive(false);
                });
        });
    }
}