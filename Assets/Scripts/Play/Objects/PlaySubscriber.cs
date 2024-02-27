using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlaySubscriber : TruongMonoBehaviour, IMessageHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitUntil(() => TruongObserver.IsAvailable);
            SubscribeObserver();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (!TruongObserver.IsAvailable) return;
        UnSubscribeObserver();
    }

    protected virtual void SubscribeObserver()
    {
        TruongObserver.Instance.AddSubscriber(MessageType.OnGameStateChange, this);
        TruongObserver.Instance.AddSubscriber(MessageType.OnTimeChange, this);
        TruongObserver.Instance.AddSubscriber(MessageType.OnHeroStateChange, this);
    }

    protected virtual void UnSubscribeObserver()
    {
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnGameStateChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnTimeChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnHeroStateChange, this);
    }

    public void HandleMessage(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGameStateChange:
                OnGameStateChange(message.data[0].ToString());
                break;

            case MessageType.OnTimeChange:
                OnTimeChange((int)message.data[0]);
                break;

            case MessageType.OnHeroStateChange:
                Enum.TryParse(message.data[0].ToString(), out HeroState.StateType value);
                OnHeroStateChange(value);
                break;
        }
    }


    protected virtual void OnGameStateChange(string value)
    {
        // For Override
    }

    protected virtual void OnTimeChange(int value)
    {
        // For Override
    }

    protected virtual void OnHeroStateChange(HeroState.StateType value)
    {
        // For Override
    }
}