using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class TruongUIPopup : TruongUIButton
{
    [TitleGroup("TruongUI")]
    private const float Delay = 0.25f;
    [SerializeField] private Transform content;
    public Transform Content => content;

    [SerializeField] private Transform title;
    public Transform Title => title;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPopup();
        LoadTitleText();
    }

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        SetTitleText();
    }

    private void LoadTitleText()
    {
        title = transform.Find(TruongConstants.CONTENT).Find(TruongConstants.TITLE);
    }

    private void LoadPopup()
    {
        content = transform.Find(TruongConstants.CONTENT);
    }

    protected abstract void SetTitleText();

    protected virtual void SetTitleText(string value)
    {
        this.title.GetComponentInChildren<TextMeshProUGUI>().text = value;
    }


    [Button]
    public void ShowPopup(Action action = null)
    {
        if (!Content || !Application.isPlaying) return;
        var obj = this.transform;
        if (!obj.gameObject.GetComponent<CanvasGroup>()) obj.gameObject.AddComponent<CanvasGroup>();
        var id = obj.name;
        obj.gameObject.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0f;
        SetAnimation_ShowPopup(Content, obj.gameObject, action, id);
    }

    public void ShowPopup(List<Transform> panels, GameObject obj, Action action = null)
    {
        if (panels.Count == 0 || !Application.isPlaying) return;
        if (!obj.gameObject.GetComponent<CanvasGroup>()) obj.gameObject.AddComponent<CanvasGroup>();
        var id = obj.name;
        obj.gameObject.SetActive(true);
        obj.GetComponent<CanvasGroup>().alpha = 0f;
        for (int i = 0; i < panels.Count; i++)
        {
            SetAnimation_ShowPopup(panels[i], obj, action, id + i);
        }
    }

    private void SetAnimation_ShowPopup(Transform panel, GameObject obj, Action action = null, string id = "")
    {
        Debug.Log($"ShowPopup {id}");
        panel.localScale = Vector3.one * 0.1f;
        DOTween.Kill($"hide_popup_{id}");
        Sequence seq = DOTween.Sequence().SetId($"show_popup_{id}");
        seq.Append(panel.DOScale(1.1f, Delay * 2 / 3))
            .Append(panel.DOScale(1f, Delay * 1 / 3))
            .Insert(0f, obj.GetComponent<CanvasGroup>().DOFade(1f, Delay * 2 / 3)).OnComplete(() =>
            {
                action?.Invoke();
                OnPopupShown();
            });
    }

    public virtual void OnPopupShown()
    {
        //For override
    }

    [Button]
    public void HidePopup(Action action = null)
    {
        if (!Content || !Application.isPlaying) return;
        var obj = this.transform;
        if (!obj.gameObject.GetComponent<CanvasGroup>()) obj.gameObject.AddComponent<CanvasGroup>();
        var id = obj.name;
        obj.GetComponent<CanvasGroup>().alpha = 1f;
        SetAnimation_HidePopup(Content, obj.gameObject, action);
    }

    public void HidePopup(List<Transform> panels, GameObject obj, Action action = null)
    {
        if (panels.Count == 0 || !Application.isPlaying) return;
        if (!obj.gameObject.GetComponent<CanvasGroup>()) obj.gameObject.AddComponent<CanvasGroup>();
        var id = obj.name;
        obj.GetComponent<CanvasGroup>().alpha = 1f;
        for (int i = 0; i < panels.Count; i++)
        {
            SetAnimation_HidePopup(panels[i], obj, action, id + i);
        }
    }

    private void SetAnimation_HidePopup(Transform panel, GameObject obj, Action action = null, string id = "")
    {
        panel.localScale = Vector3.one;
        Debug.Log($"HidePopup {id}");
        DOTween.Kill($"show_popup_{id}");
        Sequence seq = DOTween.Sequence().SetId($"hide_popup_{id}");
        seq.Append(panel.DOScale(1.1f, Delay * 1 / 3))
            .Append(panel.DOScale(0.1f, Delay * 2 / 3))
            .Insert(Delay * 1 / 3, obj.GetComponent<CanvasGroup>().DOFade(0f, Delay * 2 / 3))
            .OnComplete(() =>
            {
                obj.gameObject.SetActive(false);
                action?.Invoke();
                OnPopupHidden();
            });
    }

    public virtual void OnPopupHidden()
    {
    }
}