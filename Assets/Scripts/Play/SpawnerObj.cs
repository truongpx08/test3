using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnerObj : TruongSpawner, IMessageHandler
{
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            yield return new WaitUntil(() => TruongObserver.IsAvailable);
            TruongObserver.Instance.AddSubscriber(MessageType.OnGameStateChange, this);
            TruongObserver.Instance.AddSubscriber(MessageType.OnTimeChange, this);
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        if (!TruongObserver.IsAvailable) return;
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnGameStateChange, this);
        TruongObserver.Instance.RemoveSubscriber(MessageType.OnTimeChange, this);
    }

    public void HandleMessage(Message message)
    {
        switch (message.type)
        {
            case MessageType.OnGameStateChange:
                OnStateChange(message.data[0].ToString());
                break;

            case MessageType.OnTimeChange:
                OnTimeChange((int)message.data[0]);
                break;
        }
    }


    protected abstract void OnTimeChange(int value);

    protected abstract void OnStateChange(string value);
}