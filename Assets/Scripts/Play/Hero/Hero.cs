using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Hero : PlayObject
{
    [TitleGroup("Ref")]
    [SerializeField] protected SpriteRenderer model;
    [TitleGroup("Data")]
    [SerializeField] protected HeroData data;
    public HeroData Data => data;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
    }

    private void LoadModel()
    {
        this.model = this.transform.Find(TruongChildName.Model).GetComponent<SpriteRenderer>();
    }

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        AddName();
        AddColor();
        AddHp();
        AddAtk();
        AddDurationAnim();
    }


    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        switch (value)
        {
            case HeroState.StateType.Move:
                Move();
                break;

            case HeroState.StateType.Attack:
                Attack();
                break;
        }
    }

    protected abstract void AddName();

    protected void SetName(string value)
    {
        Debug.Log("Setting name");
        this.data.name = value;
    }

    protected abstract void AddColor();

    protected void SetColor(Color red)
    {
        this.model.color = red;
    }

    public void AddCurrentCell(Cell value)
    {
        this.data.currentCell = value;
    }

    private void AddAtk()
    {
        this.data.atk = 2;
    }

    private void AddHp()
    {
        this.data.hp = 10;
    }

    private void AddDurationAnim()
    {
        this.data.durationAnim = 0.5f;
    }

    protected abstract void Move();

    protected void JumpNextCell()
    {
        this.data.nextCell = GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.Data.type == CellType.ReserveEnemy) return;
        if (this.data.nextCell.Data.type == CellType.ReserveAlly) return;
        if (this.data.nextCell.HasHero) return;

        this.data.subsequentCell = GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (this.data.subsequentCell.HasHero) return;

        PlayAnimJump(this.data.nextCell);
    }

    protected abstract Cell GetNextCell();
    protected abstract Cell GetSubsequentCell();

    protected void PlayAnimJump(Cell nextCell)
    {
        SetIsInStatus(true);
        this.transform.DOMove(nextCell.gameObject.transform.position, data.durationAnim)
            .OnComplete(() =>
            {
                var thisTransform = this.transform;
                thisTransform.parent = nextCell.HeroSpawner.Holder.transform;
                this.data.currentCell.HeroSpawner.Holder.Items.Clear();
                nextCell.HeroSpawner.Holder.Items.Add(thisTransform);
                AddCurrentCell(nextCell);
                SetIsInStatus(false);
            });
    }

    private void SetIsInStatus(bool value)
    {
        this.data.isInStatus = value;
    }

    private void Attack()
    {
        this.data.nextCell = GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.HasHero)
        {
            PlayAnimAttack(this.data.nextCell.Hero);
            return;
        }

        this.data.subsequentCell = GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (!this.data.subsequentCell.HasHero) return;

        PlayAnimAttack(this.data.subsequentCell.Hero);
    }

    private void PlayAnimAttack(Hero target)
    {
        Debug.Log("Attack ");
        SetIsInStatus(true);
        DOVirtual.DelayedCall(data.durationAnim, () =>
        {
            target.Hurt(this.data.atk);
            SetIsInStatus(false);
        });
    }

    private void Hurt(int value)
    {
        Debug.Log("Hurt " + value);
        this.data.hp -= value;
        if (this.data.hp == 0) Died();
    }

    [Button]
    private void Died()
    {
        DOVirtual.DelayedCall(0.1f, () => { this.data.currentCell.HeroDespawner.DespawnObject(this.transform); });
    }
}