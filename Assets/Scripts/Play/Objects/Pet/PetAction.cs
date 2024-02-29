using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract class PetAction : PetRefAbstract
{
    protected void CallActionWithDelay(Action action)
    {
        this.pet.Init.SetIsActive(true);
        DOVirtual.DelayedCall(this.pet.Data.durationAnim, () =>
        {
            action?.Invoke();
            this.pet.Init.SetIsActive(false);
        });
    }

    protected void CallAction(Action action)
    {
        this.pet.Init.SetIsActive(true);
        action?.Invoke();
    }

    protected bool HasEnemyAtCell(Cell cell)
    {
        return cell.PetSpawner.Holder.Items.Count != 0 &&
               cell.PetSpawner.Holder.Items.Any(h =>
                   h.gameObject.activeSelf && h.GetComponent<Pet>().Data.type != this.Data.type);
    }

    protected bool HasAllyAtCell(Cell cell)
    {
        return cell.PetSpawner.Holder.Items.Count != 0 &&
               cell.PetSpawner.Holder.Items.Any(h =>
                   h.gameObject.activeSelf && h.GetComponent<Pet>().Data.type == this.Data.type);
    }
}