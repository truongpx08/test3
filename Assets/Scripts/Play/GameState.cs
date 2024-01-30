using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameState : State
{
    public const string OnStart = "OnStart";
    public const string OnUpdate = "OnUpdate";
    public const string OnEnd = "OnEnd";

    protected override void Start()
    {
        base.Start();
        //wait objects subscribe observer
        DOVirtual.DelayedCall(0.5f, () => SendStateToSubscribers(OnStart)).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1f, () => SendStateToSubscribers(OnUpdate));
        });
    }

    protected override void OnTimeChange(int value)
    {
        if (value != 0) return;
        SendStateToSubscribers(OnEnd);
    }

    protected override void OnGameStateChange(string value)
    {
        SetCurrentState(value);
    }

    protected override void SendStateToSubscribers(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnGameStateChange,
            new object[] { value }));
    }
}