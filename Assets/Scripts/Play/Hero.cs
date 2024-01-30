using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Hero : PlayObject
{
    [TitleGroup("Ref")]
    [SerializeField] protected SpriteRenderer model;
    [TitleGroup("Data")]
    [SerializeField] protected HeroData data;

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

            case HeroState.OnAttack:
                Attack();
                break;
        }
    }

    protected abstract void SetColor();

    public void SetAtTile(Cell value)
    {
        this.data.currentCell = value;
    }

    protected abstract Cell GetCellToJump();
    protected abstract Cell GetNextCellToJump();

    public void Spawn(Cell cell)
    {
        this.data = new HeroData
        {
            hp = 10,
            atk = 1
        };
        SetAtTile(cell);
    }

    protected void Jump()
    {
        this.data.cellToJump = GetCellToJump();
        if (this.data.cellToJump == null) return;
        if (this.data.cellToJump.HasHero) return;

        this.data.nextCellToJump = GetNextCellToJump();
        if (this.data.nextCellToJump == null) return;
        if (this.data.nextCellToJump.HasHero) return;

        this.transform.DOMove(this.data.cellToJump.gameObject.transform.position, HeroState.TimeJump).OnComplete(() =>
        {
            var thisTransform = this.transform;
            thisTransform.parent = this.data.cellToJump.HeroSpawner.Holder.transform;
            this.data.currentCell.HeroSpawner.Holder.Items.Clear();
            this.data.cellToJump.HeroSpawner.Holder.Items.Add(thisTransform);
            SetAtTile(this.data.cellToJump);
        });
    }

    private void Attack()
    {
        if (this.data.cellToJump == null && this.data.nextCellToJump == null) return;
        Hero hero;
        if (this.data.cellToJump.HasHero)
        {
            hero = this.data.cellToJump.Hero;
            hero.Hurt(this.data.atk);
            return;
        }

        hero = this.data.nextCellToJump.Hero;
        hero.Hurt(this.data.atk);
    }

    private void Hurt(int value)
    {
        this.data.hp -= value;
        if (this.data.hp == 0) Died();
    }

    private void Died()
    {
        this.data.currentCell.HeroDespawner.DespawnDefaultObject();
    }
}