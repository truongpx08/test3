using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
        DOVirtual.DelayedCall(0.5f, () => NotifyToSubscribers(OnStart));
    }

    protected override void OnTimeChange(int value)
    {
        if (value == PlayObjects.Instance.Time.MaxTime - 1)
        {
            NotifyToSubscribers(OnUpdate);
            return;
        }

        if (value == 0) NotifyToSubscribers(OnEnd);
    }

    protected override void OnGameStateChange(string value)
    {
        SetCurrentState(value);
    }

    protected override void NotifyToSubscribers(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnGameStateChange,
            new object[] { value }));
    }
}