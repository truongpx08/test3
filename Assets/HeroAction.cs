using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public abstract class HeroAction : HeroRefAbstract
{
    protected void CallActionWithDelay(Action action)
    {
        this.hero.Init.SetIsActive(true);
        DOVirtual.DelayedCall(this.hero.Data.durationAnim, () =>
        {
            action?.Invoke();
            this.hero.Init.SetIsActive(false);
        });
    }

    protected void CallAction(Action action)
    {
        this.hero.Init.SetIsActive(true);
        action?.Invoke();
    }

    protected bool HasEnemyAtCell(Cell cell)
    {
        return cell.HeroSpawner.Holder.Items.Count != 0 &&
               cell.HeroSpawner.Holder.Items.Any(h =>
                   h.gameObject.activeSelf && h.GetComponent<Hero>().name != this.Data.name);
    }

    protected bool HasAllyAtCell(Cell cell)
    {
        return cell.HeroSpawner.Holder.Items.Count != 0 &&
               cell.HeroSpawner.Holder.Items.Any(h =>
                   h.gameObject.activeSelf && h.GetComponent<Hero>().name == this.Data.name);
    }
}