using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class Hero : GameObj
{
    [SerializeField] protected Cell atCell;
    [SerializeField] protected SpriteRenderer model;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
    }

    private void LoadModel()
    {
        this.model = this.transform.Find(TruongChildName.Model).GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        SetColor();
    }

    protected override void OnHeroStateChange(string value)
    {
        base.OnHeroStateChange(value);
        switch (value)
        {
            case HeroState.OnJump:
                Jump();
                break;
        }
    }

    protected abstract void SetColor();

    public void SetAtTile(Cell value)
    {
        this.atCell = value;
    }

    protected abstract Cell GetCellToJump();

    protected void Jump()
    {
        var target = GetCellToJump();
        if (target == null) return;
        this.transform.DOMove(target.gameObject.transform.position, 0.25f).OnComplete(() => SetAtTile(target));
    }
}