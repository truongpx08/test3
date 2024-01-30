using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeroState : State
{
    public const string OnJump = "OnJump";
    public const string OnAttack = "OnAttack";

    protected override void OnTimeChange(int value)
    {
        SendStateToSubscribers(OnJump);
        DOVirtual.DelayedCall(0.25f, () => SendStateToSubscribers(OnAttack));
    }

    protected override void OnHeroStateChange(string value)
    {
        SetCurrentState(value);
    }

    protected override void SendStateToSubscribers(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnHeroStateChange,
            new object[] { value }));
    }
}