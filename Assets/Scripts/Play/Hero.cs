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

    protected override void Start()
    {
        base.Start();
        SetColor();
    }

    protected abstract void SetColor(); 

    private void LoadModel()
    {
        this.model = this.transform.Find(TruongChildName.Model).GetComponent<SpriteRenderer>();
    }

    public void SetAtTile(Cell value)
    {
        this.atCell = value;
    }

    protected override void OnTimeChange(int value)
    {
        Jump();
    }

    protected void Jump()
    {
        var target = GetCellToJump();
        if (target == null) return;
        this.transform.DOMove(target.gameObject.transform.position, 0.25f).OnComplete(() => SetAtTile(target));
    }

    protected abstract Cell GetCellToJump();

    protected override void OnStateChange(string value)
    {
    }
}