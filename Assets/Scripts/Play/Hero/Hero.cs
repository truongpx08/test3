using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private HeroAttacker attacker;
    public HeroAttacker Attacker => attacker;

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
    }

    private void LoadMovement()
    {
        this.movement = GetComponentInChildren<HeroMovement>();
    }

    private void LoadAttacker()
    {
        this.attacker = GetComponentInChildren<HeroAttacker>();
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
    protected abstract void Move();
    public abstract Cell GetNextCell();
    public abstract Cell GetSubsequentCell();

    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        switch (value)
        {
            case HeroState.StateType.Move:
                Move();
                break;

            case HeroState.StateType.Attack:
                this.attacker.Attack();
                break;
        }
    }
}