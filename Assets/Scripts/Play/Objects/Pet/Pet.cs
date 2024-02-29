using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Pet : PlaySubscriber
{
    [TitleGroup("Data")]
    [SerializeField] protected PetData data;
    public PetData Data => data;
    [TitleGroup("Ref")]
    [SerializeField] protected SpriteRenderer model;
    public SpriteRenderer Model => model;
    [SerializeField] protected HpText hpText;
    public HpText HpText => hpText;
    [SerializeField] protected AtkText atkText;
    public AtkText AtkText => atkText;
    [SerializeField] private PetInit init;
    public PetInit Init => init;
    [SerializeField] private PetMovement movement;
    public PetMovement Movement => movement;
    [SerializeField] private PetAttack attack;
    public PetAttack Attack => attack;
    [SerializeField] private PetInjury injury;
    public PetInjury Injury => injury;
    [SerializeField] private PetFaintness faintness;
    public PetFaintness Faintness => faintness;

    [SerializeField] private PetBulletSpawner bulletSpawner;
    public PetBulletSpawner BulletSpawner => bulletSpawner;
    [SerializeField] private PetBulletDespawner bulletDespawner;
    public PetBulletDespawner BulletDespawner => bulletDespawner;

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
        this.bulletDespawner = GetComponentInChildren<PetBulletDespawner>();
    }

    private void LoadBulletSpawner()
    {
        this.bulletSpawner = GetComponentInChildren<PetBulletSpawner>();
    }

    private void LoadFaintness()
    {
        this.faintness = GetComponentInChildren<PetFaintness>();
    }

    private void LoadInjury()
    {
        this.injury = GetComponentInChildren<PetInjury>();
    }

    private void LoadMovement()
    {
        this.movement = GetComponentInChildren<PetMovement>();
    }

    private void LoadAttacker()
    {
        this.attack = GetComponentInChildren<PetAttack>();
    }

    private void LoadInit()
    {
        this.init = GetComponentInChildren<PetInit>();
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

    protected override void OnPetStateChange(PetState.StateType value)
    {
        base.OnPetStateChange(value);
        switch (value)
        {
            case PetState.StateType.BeforeMove:
                break;

            case PetState.StateType.Move:
                this.Movement.TryMove();
                break;

            case PetState.StateType.Attack:
                this.Attack.TryAttack();
                break;

            case PetState.StateType.Injury:
                this.Injury.TryHurt();
                break;

            case PetState.StateType.Faint:
                this.Faintness.TryFaint();
                break;

            case PetState.StateType.None:
            case PetState.StateType.AfterFaint:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(value), value, null);
        }
    }


    public Cell GetNextCell()
    {
        switch (this.Data.type)
        {
            case PetType.Bot:
                return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.botNextCellId);

            case PetType.Top:
                return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.currentCell.Data.topNextCellId);
        }

        return null;
    }

    public Cell GetSubsequentCell()
    {
        switch (this.Data.type)
        {
            case PetType.Bot:
                return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.botNextCellId);

            case PetType.Top:
                return PlayObjects.Instance.CellSpawner.GetCellWithId(this.Data.nextCell.Data.topNextCellId);
        }

        return null;
    }

    public string GetReserveType()
    {
        switch (this.Data.type)
        {
            case PetType.Bot:
                return CellType.BotReserve;

            case PetType.Top:
                return CellType.TopReserve;
        }

        return null;
    }

    public bool IsTeammate(Pet pet)
    {
        return this.Data.type == pet.Data.type;
    }

    public bool IsOpponent(Pet pet)
    {
        return this.Data.type != pet.Data.type;
    }

    public void AddDataWithPetId(int petId)
    {
        var petData = PlayData.Instance.PetsData.GetPetDataWithId(petId);
        this.data = new PetData
        {
            id = petData.id,
            icon = petData.icon,
            hp = petData.hp,
            atk = petData.atk,
        };
    }
}