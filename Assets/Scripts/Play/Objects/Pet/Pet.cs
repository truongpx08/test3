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
    [SerializeField] protected PetHp hp;
    public PetHp Hp => hp;
    [SerializeField] protected PetAtk atk;
    public PetAtk Atk => atk;
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
    [SerializeField] private PetAbility ability;
    public PetAbility Ability => ability;

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
        this.atk = GetComponentInChildren<PetAtk>();
    }

    private void LoadHpText()
    {
        this.hp = GetComponentInChildren<PetHp>();
    }

    private void LoadModel()
    {
        this.model = this.transform.Find(TruongChildName.Model).GetComponent<SpriteRenderer>();
    }

    public void AddDataWithPetId(int petId)
    {
        var petData = PlayData.Instance.PetsData.GetPetDataWithId(petId);
        this.data = new PetData
        {
            id = petData.id,
            icon = petData.icon,
            ability = petData.ability,
            hp = petData.hp,
            atk = petData.atk,
        };
    }

    public void AddAbility()
    {
        this.ability = Instantiate(this.Data.ability, transform);
    }

    protected override void OnPetStateChange(PetState.StateType state)
    {
        base.OnPetStateChange(state);
        switch (state)
        {
            case PetState.StateType.None:
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

            case PetState.StateType.AfterFaint:
            case PetState.StateType.BeforeMove:
            case PetState.StateType.AfterMove:
                if (this.ability == null) break;
                this.Ability.TryUse(state, this);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
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
}