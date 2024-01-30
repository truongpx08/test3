using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Cell : PlayObject
{
    [TitleGroup("Ref")]
    [SerializeField] private CellHeroSpawner heroSpawner;
    public CellHeroSpawner HeroSpawner => heroSpawner;
    [SerializeField] private CellHeroDespawner heroDespawner;
    public CellHeroDespawner HeroDespawner => heroDespawner;
    [SerializeField] private TextMeshPro tileAllyIdToJumpTMP;
    [SerializeField] private TextMeshPro tileEnemyIdToJumpTMP;
    [SerializeField] private TextMeshPro idTMP;
    [TitleGroup("Data")]
    [SerializeField] private CellData data;
    public CellData Data => data;
    public bool HasHero => heroSpawner.Holder.Items.Count != 0;
    public Hero Hero => heroSpawner.Holder.Items[0].GetComponent<Hero>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadAllyTileIdToJump();
        LoadEnemyTileIdToJump();
        LoadIdTMP();
        LoadHeroSpawner();
        LoadHeroDespawner();
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

    public void SetData(CellData cellData)
    {
        this.data = cellData;
    }


    public void SetName()
    {
        this.name = $"Cell c{this.data.column}, y{this.data.row}, id{this.data.id}";
        this.idTMP.text = this.data.id.ToString();
    }

    public void SetType(string value)
    {
        this.data.type = value;
    }

    public void SetAllyIdCellToJump(int value)
    {
        this.data.cellToJumpOfAlly = value;
        this.tileAllyIdToJumpTMP.text = value.ToString();
    }

    public void SetEnemyIdCellToJump(int value)
    {
        this.data.cellToJumpOfEnemy = value;
        this.tileEnemyIdToJumpTMP.text = value.ToString();
    }
}