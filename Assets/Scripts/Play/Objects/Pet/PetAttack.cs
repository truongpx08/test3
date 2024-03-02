using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PetAttack : PetAction
{
    public void TryAttack()
    {
        if (this.PetData.isBoss) return;
        if (this.PetData.currentCell.Data.type == pet.Data.finishCellType)
        {
            var boss = PetReference.Instance.GetBoss(this.PetData.type);
            if (boss.Data.hp <= 0) return;
            Attack(boss);
            return;
        }

        this.PetData.nextCell = this.pet.GetNextCell();

        if (this.PetData.nextCell == null) return;
        if (HasAllyAtCell(this.PetData.nextCell)) return;
        if (HasEnemyAtCell(this.PetData.nextCell))
        {
            Attack(this.PetData.nextCell.Pet);
            return;
        }


        this.PetData.subsequentCell = this.pet.GetSubsequentCell();
        if (this.PetData.subsequentCell == null) return;
        if (!HasEnemyAtCell(this.PetData.subsequentCell)) return;

        Attack(this.PetData.subsequentCell.Pet);
    }


    private void Attack(Pet target)
    {
        CallAction(() =>
        {
            var bullet = this.pet.BulletSpawner.SpawnDefaultObject();
            bullet.transform.position = this.pet.transform.position;
            bullet.transform.DOMove(target.gameObject.transform.position, this.PetData.durationAnim)
                .OnComplete(() =>
                {
                    target.Injury.SetWasAttacked(true);
                    target.Injury.SetDamageReceived(this.PetData.atk);
                    this.pet.BulletDespawner.DespawnDefaultObject();
                    this.pet.Init.SetIsActive(false);
                });
        });
    }
}