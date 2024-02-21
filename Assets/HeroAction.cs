using System;
using System.Collections;
using System.Collections.Generic;
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
}