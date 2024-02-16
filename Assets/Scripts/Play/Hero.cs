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

    protected abstract void SetColor();

    private void SetCurrentCell(Cell value)
    {
        this.data.currentCell = value;
    }

    protected abstract Cell GetNextCell();
    protected abstract Cell GetSubsequentCell();

    public void Spawn(Cell cell)
    {
        this.data = new HeroData
        {
            hp = 10,
            atk = 1
        };
        SetCurrentCell(cell);
    }

    protected override void OnTimeChange(int value)
    {
        base.OnTimeChange(value);
        Jump();
    }

    protected abstract void Jump();

    protected void JumpNextCell()
    {
        this.data.nextCell = GetNextCell();
        if (this.data.nextCell == null) return;
        if (this.data.nextCell.Data.type == CellType.ReserveEnemy) return;
        if (this.data.nextCell.Data.type == CellType.ReserveAlly) return;
        if (this.data.nextCell.HasHero)
        {
            Attack(this.data.nextCell.Hero);
            return;
        }

        this.data.subsequentCell = GetSubsequentCell();
        if (this.data.subsequentCell == null) return;
        if (this.data.subsequentCell.HasHero)
        {
            Attack(this.data.subsequentCell.Hero);
            return;
        }

        JumpToCell(this.data.nextCell);
    }

    protected void JumpToCell(Cell cell)
    {
        this.transform.DOMove(cell.gameObject.transform.position, 0.25f).OnComplete(() =>
        {
            var thisTransform = this.transform;
            thisTransform.parent = cell.HeroSpawner.Holder.transform;
            this.data.currentCell.HeroSpawner.Holder.Items.Clear();
            cell.HeroSpawner.Holder.Items.Add(thisTransform);
            SetCurrentCell(cell);
        });
    }

    private void Attack(Hero target)
    {
        target.Hurt(this.data.atk);
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