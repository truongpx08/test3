using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameState : State
{
    [SerializeField] private bool isFirstS;
    public const string OnStart = "OnStart";
    public const string OnUpdate = "OnUpdate";
    public const string OnEnd = "OnEnd";

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        isFirstS = true;
    }

    protected override void Start()
    {
        base.Start();
        //wait objects subscribe observer
        DOVirtual.DelayedCall(0.5f, () => SendStateToSubscribers(OnStart));
    }

    protected override void OnTimeChange(int value)
    {
        if (value == PlayObjects.Instance.Time.MaxTime - 1)
        {
            SendStateToSubscribers(OnUpdate);
            return;
        }

        if (value == 0) SendStateToSubscribers(OnEnd);
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