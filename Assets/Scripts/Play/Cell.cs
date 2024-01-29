using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Cell : SpawnerObj
{
    [SerializeField] private CellData data;
    public CellData Data => data;
    [SerializeField] private TextMeshPro tileIdToJumpTMP;
    [SerializeField] private TextMeshPro idTMP;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileIdToJump();
        LoadIdTMP();
    }

    private void LoadIdTMP()
    {
        this.idTMP = transform.Find("IdTMP").GetComponent<TextMeshPro>();
    }

    private void LoadTileIdToJump()
    {
        this.tileIdToJumpTMP = transform.Find("TileIdToJump").GetComponent<TextMeshPro>();
    }

    public void SetData(CellData cellData)
    {
        this.data = cellData;
    }

    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName("Hero");
    }

    protected override void OnTimeChange(int value)
    {
    }

    protected override void OnStateChange(string value)
    {
    }

    [Button]
    private void SpawnHero()
    {
        var go = SpawnDefaultObject();
        go.GetComponent<Hero>().SetAtTile(this);
    }

    public void SetName()
    {
        this.name = $"Cell c{this.data.column}, y{this.data.row}, id{this.data.id}";
        this.idTMP.text = this.data.id.ToString() ;
    }

    public void SetType(string value)
    {
        this.data.type = value;
    }

    public void SetAllyIdCellToJump(int value)
    {
        this.data.nextCellToJumpOfAlly = value;
        this.tileIdToJumpTMP.text = value.ToString();
    }
}