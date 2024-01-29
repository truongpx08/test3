using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameState : GameObj
{
    public const string OnStart = "OnStart";
    public const string OnUpdate = "OnUpdate";
    public const string OnEnd = "OnEnd";
    [SerializeField] private string currentState;

    protected override void Start()
    {
        base.Start();
        //wait objects subscribe observer
        DOVirtual.DelayedCall(0.5f, () => SetCurrentState(OnStart)).OnComplete(() =>
        {
            DOVirtual.DelayedCall(1f, () => SetCurrentState(OnUpdate));
        });
    }

    [Button]
    private void SetCurrentState(string value)
    {
        TruongObserver.Instance.Notify(new Message(MessageType.OnStateChange,
            new object[] { value }));
    }

    protected override void OnTimeChange(int value)
    {
        if (value != 0) return;
        SetCurrentState(OnEnd);
    }

    protected override void OnStateChange(string value)
    {
        this.currentState = value;
    }
}