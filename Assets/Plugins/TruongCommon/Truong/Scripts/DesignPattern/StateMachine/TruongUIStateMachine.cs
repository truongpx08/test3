using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TruongUIStateMachine : TruongStateMachine
{
    protected override void OnStateChanged(Transform preState, Transform curState)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            base.DisableOtherState(preState);
            base.EnableCurrentState(curState);
            return;
        }

        if (!preState)
        {
            ShowPopupCurrentState(curState);
            return;
        }

        preState.GetComponent<TruongUIPopup>()
            ?.HidePopup(() => { ShowPopupCurrentState(curState); });
    }

    protected override void DisableOtherState(Transform state)
    {
        //Now the UI will be disabled automatically after running the HidePopup animation
    }

    protected override void EnableCurrentState(Transform state)
    {
        //Now the UI will be enabled automatically after running the ShowPopup animation
    }

    private void ShowPopupCurrentState(Transform curState)
    {
        if (!curState) return;
        curState.GetComponent<TruongUIPopup>()?.ShowPopup();
    }
}