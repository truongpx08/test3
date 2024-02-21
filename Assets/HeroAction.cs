using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroAction : HeroRefAbstract
{
    public void CallActionWithDelay(Action action)
    {
        this.hero.Init.SetIsActive(true);
        DOVirtual.DelayedCall(this.hero.Init.Data.durationAnim, () =>
        {
            action?.Invoke();
            this.hero.Init.SetIsActive(false);
        });
    }

    public void CallAction(Action action)
    {
        this.hero.Init.SetIsActive(true);
        action?.Invoke();
    }
}