using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PetAttack : PetAction
{
    public void TryAttack()
    {
        if (this.Data.isBoss) return;
        if (this.Data.currentCell.Data.type == pet.Data.finishCellType)
        {
            var boss = PetReference.Instance.GetBoss(this.Data.type);
            if (boss.Data.hp <= 0) return;
            Attack(boss);
            return;
        }

        this.Data.nextCell = this.pet.GetNextCell();

        if (this.Data.nextCell == null) return;
        if (HasAllyAtCell(this.Data.nextCell)) return;
        if (HasEnemyAtCell(this.Data.nextCell))
        {
            Attack(this.Data.nextCell.Pet);
            return;
        }


        this.Data.subsequentCell = this.pet.GetSubsequentCell();
        if (this.Data.subsequentCell == null) return;
        if (!HasEnemyAtCell(this.Data.subsequentCell)) return;

        Attack(this.Data.subsequentCell.Pet);
    }


    private void Attack(Pet target)
    {
        CallAction(() =>
        {
            var bullet = this.pet.BulletSpawner.SpawnDefaultObject();
            bullet.transform.position = this.pet.transform.position;
            bullet.transform.DOMove(target.gameObject.transform.position, this.Data.durationAnim)
                .OnComplete(() =>
                {
                    target.Injury.SetWasAttacked(true);
                    target.Injury.SetDamageReceived(this.Data.atk);
                    this.pet.BulletDespawner.DespawnDefaultObject();
                    this.pet.Init.SetIsActive(false);
                });
        });
    }
}