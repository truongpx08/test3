using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time : PlayObject
{
    [SerializeField] private int time;
    [SerializeField] private int maxTime;
    public int MaxTime => maxTime;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        maxTime = 9000;
    }

    protected override void OnTimeChange(int value)
    {
        this.time = value;
    }

    protected override void OnGameStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        StartCoroutine(CountDown());

        IEnumerator CountDown()
        {
            SetValue(this.maxTime);
            for (int i = 0; i < maxTime; i++)
            {
                yield return new WaitForSeconds(1);
                SetValue(this.time - 1);
            }
        }
    }
    

    private void SetValue(int value)
    {
        TruongObserver.Instance.Notify(MessageType.OnTimeChange, new object[] { value });
    }
}