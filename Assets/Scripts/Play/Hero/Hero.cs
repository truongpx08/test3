using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Hero : PlayObject
{
    [TitleGroup("Ref")]
    [SerializeField] protected SpriteRenderer model;
    public SpriteRenderer Model => model;
    [SerializeField] protected HpText hpText;
    public HpText HpText => hpText;
    [SerializeField] protected AtkText atkText;
    public AtkText AtkText => atkText;
    [SerializeField] private HeroInit init;
    public HeroInit Init => init;
    [SerializeField] private HeroMovement movement;
    public HeroMovement Movement => movement;
    [SerializeField] private HeroAttack attack;
    public HeroAttack Attack => attack;
    [SerializeField] private HeroInjury injury;
    public HeroInjury Injury => injury;
    [SerializeField] private HeroAction action;
    public HeroAction Action => action;

    [TitleGroup("Other")]
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadHpText();
        LoadAtkText();
        LoadInit();
        LoadMovement();
        LoadAttacker();
        LoadInjury();
        LoadAction();
    }

    private void LoadAction()
    {
        this.action = GetComponentInChildren<HeroAction>();
    }

    private void LoadInjury()
    {
        this.injury = GetComponentInChildren<HeroInjury>();
    }

    private void LoadMovement()
    {
        this.movement = GetComponentInChildren<HeroMovement>();
    }

    private void LoadAttacker()
    {
        this.attack = GetComponentInChildren<HeroAttack>();
    }

    private void LoadInit()
    {
        this.init = GetComponentInChildren<HeroInit>();
    }

    private void LoadAtkText()
    {
        this.atkText = GetComponentInChildren<AtkText>();
    }

    private void LoadHpText()
    {
        this.hpText = GetComponentInChildren<HpText>();
    }

    private void LoadModel()
    {
        this.model = this.transform.Find(TruongChildName.Model).GetComponent<SpriteRenderer>();
    }

    public abstract void AddName();
    public abstract void AddColor();
    public abstract Cell GetNextCell();
    public abstract Cell GetSubsequentCell();
    public abstract string GetReserveCellType();
    public abstract string GetSpawnPointCellType();

    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        switch (value)
        {
            case HeroState.StateType.Movement:
                this.Movement.TryMove();
                break;

            case HeroState.StateType.Attack:
                this.Attack.TryAttack();
                break;
            
            case HeroState.StateType.Injury:
                this.Injury.TryHurt();
                break;
        }
    }
}