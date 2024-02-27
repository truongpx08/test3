using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Hero : PlaySubscriber
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
    [SerializeField] private HeroFaintness faintness;
    public HeroFaintness Faintness => faintness;

    [TitleGroup("Data")]
    [SerializeField] protected HeroData data;
    [SerializeField] private HeroBulletSpawner bulletSpawner;
    public HeroBulletSpawner BulletSpawner => bulletSpawner;
    public HeroData Data => data;
    [SerializeField] private HeroBulletDespawner bulletDespawner;
    public HeroBulletDespawner BulletDespawner => bulletDespawner;

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
        LoadFaintness();
        LoadBulletSpawner();
        LoadBulletDespawner();
    }

    private void LoadBulletDespawner()
    {
        this.bulletDespawner = GetComponentInChildren<HeroBulletDespawner>();
    }

    private void LoadBulletSpawner()
    {
        this.bulletSpawner = GetComponentInChildren<HeroBulletSpawner>();
    }

    private void LoadFaintness()
    {
        this.faintness = GetComponentInChildren<HeroFaintness>();
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

    protected override void OnHeroStateChange(HeroState.StateType value)
    {
        base.OnHeroStateChange(value);
        switch (value)
        {
            case HeroState.StateType.BeforeMove:
                break;
            
            case HeroState.StateType.Move:
                this.Movement.TryMove();
                break;

            case HeroState.StateType.Attack:
                this.Attack.TryAttack();
                break;

            case HeroState.StateType.Injury:
                this.Injury.TryHurt();
                break;

            case HeroState.StateType.Faint:
                this.Faintness.TryFaint();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }


    public abstract void AddType();
    public abstract void AddColor();
    public abstract Cell GetNextCell();
    public abstract Cell GetSubsequentCell();
    public abstract string GetReserveType();

    public bool IsTeammate(Hero hero)
    {
        return this.Data.type == hero.Data.type;
    }

    public bool IsOpponent(Hero hero)
    {
        return this.Data.type != hero.Data.type;
    }
}