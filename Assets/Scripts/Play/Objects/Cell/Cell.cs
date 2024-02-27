using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class Cell : PlaySubscriber
{
    [TitleGroup("Ref")]
    [SerializeField] private CellHeroSpawner heroSpawner;
    public CellHeroSpawner HeroSpawner => heroSpawner;
    [SerializeField] private CellHeroDespawner heroDespawner;
    public CellHeroDespawner HeroDespawner => heroDespawner;

    [SerializeField] public SpriteRenderer model;
    [SerializeField] private TextMeshPro tileAllyIdToJumpTMP;
    [SerializeField] private TextMeshPro tileEnemyIdToJumpTMP;
    [SerializeField] private TextMeshPro idTMP;
    [TitleGroup("Data")]
    [SerializeField] private CellData data;
    public CellData Data => data;
    public bool HasHero => heroSpawner.Holder.Items.Any(h => h.gameObject.activeSelf);
    public Hero Hero => heroSpawner.Holder.Items.Find(h => h.gameObject.activeSelf).GetComponent<Hero>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAllyTileIdToJump();
        LoadEnemyTileIdToJump();
        LoadIdTMP();
        LoadModel();
        LoadHeroSpawner();
        LoadHeroDespawner();
    }


    private void LoadModel()
    {
        this.model = transform.Find(TruongConstants.MODEL).GetComponent<SpriteRenderer>();
    }

    private void LoadHeroDespawner()
    {
        this.heroDespawner = GetComponentInChildren<CellHeroDespawner>();
    }

    private void LoadHeroSpawner()
    {
        this.heroSpawner = GetComponentInChildren<CellHeroSpawner>();
    }

    private void LoadEnemyTileIdToJump()
    {
        this.tileEnemyIdToJumpTMP = transform.Find("TileEnemyIdToJump").GetComponent<TextMeshPro>();
    }

    private void LoadIdTMP()
    {
        this.idTMP = transform.Find("IdTMP").GetComponent<TextMeshPro>();
    }

    private void LoadAllyTileIdToJump()
    {
        this.tileAllyIdToJumpTMP = transform.Find("TileAllyIdToJump").GetComponent<TextMeshPro>();
    }

    protected override void Start()
    {
        base.Start();
        AddColor();
    }


    protected abstract void AddColor();

    public void AddData(CellData cellData)
    {
        this.data = cellData;
    }

    public void AddName()
    {
        this.name =
            $"Cell c{this.data.column}, r{this.data.row}, id{this.data.id}, allyPath{this.data.allyPathId}, enemyPath{this.data.enemyPathId}";
        this.idTMP.text = this.data.id.ToString();
    }

    private void SetType(string value)
    {
        this.data.type = value;
    }

    public void SetAllyNextCell(int value)
    {
        this.data.allyNextCell = value;
        this.tileAllyIdToJumpTMP.text = value.ToString();
    }

    public void SetEnemyNextCell(int value)
    {
        this.data.enemyNextCell = value;
        this.tileEnemyIdToJumpTMP.text = value.ToString();
    }

    protected void AddColor(Color value)
    {
        this.model.color = value;
    }

    public void AddType()
    {
        if (Data.row == 0)
        {
            SetType(CellType.ReserveEnemy);
            return;
        }

        if (Data.row == CellSpawner.Row - 1)
        {
            SetType(CellType.ReserveAlly);
            return;
        }

        if (Data.row == 1 && Data.column == 0)
        {
            SetType(CellType.AllyFinish);
            return;
        }

        if (Data.row == CellSpawner.Row - 2 && Data.column == 0)
        {
            SetType(CellType.EnemyFinish);
            return;
        }

        SetType(CellType.Combat);
    }

    public void SetAllyPathId(int value)
    {
        this.Data.allyPathId = value;
    }

    public void SetEnemyPathId(int value)
    {
        this.Data.enemyPathId = value;
    }
}