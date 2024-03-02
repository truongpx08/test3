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
        TruongObserver.Instance.AddSubscriber(MessageType.OnPetStateChange, this);
        TruongObserver.Instance.AddSubscriber(MessageType.OnManaChange, this);
        TruongObserver.Instance.AddSubscriber(MessageType.OnHpChange, this);
    }

    protected virtual void UnSubscribeObserver()
    {
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnGameStateChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnTimeChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnPetStateChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnManaChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnHpChange, this);
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

            case MessageType.OnPetStateChange:
                Enum.TryParse(message.data[0].ToString(), out PetState.StateType value);
                OnPetStateChange(value);
                break;
            case MessageType.OnManaChange:
                OnManaChange((int)message.data[0]);
                break;

            case MessageType.OnHpChange:
                OnHpChange((int)message.data[0]);
                break;
        }
    }

    protected virtual void OnHpChange(int value)
    {
        // For Override
    }

    protected virtual void OnManaChange(int value)
    {
        // For Override
    }


    protected virtual void OnGameStateChange(string value)
    {
        // For Override
    }

    protected virtual void OnTimeChange(int value)
    {
        // For Override
    }

    protected virtual void OnPetStateChange(PetState.StateType state)
    {
        // For Override
    }
}