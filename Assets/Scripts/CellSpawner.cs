using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CellSpawner : SpawnerObj
{
    [SerializeField] private int row;
    [SerializeField] private int column;
    [SerializeField] private float spacing;
    [SerializeField] private float top;
    [SerializeField] private float left;
    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        this.spacing = 1.05f;
    }
    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName("HeroHolder");
    }

    // [Button]
    // private void SpawnHeroHolder()
    // {
    //     for (int i = 0; i < this.tileCount; i++)
    //     {
    //         var go = SpawnDefaultObject();
    //         go.GetComponent<Tile>().SetIndex(i);
    //     }
    // }

    [Button]
    public void Spawn(int rowP, int columnP)
    {
        this.row = rowP;
        this.column = columnP;
        InitVarToSetPosition();
        var count = 0;

        for (int r = 0; r < rowP; r++)
        {
            for (int c = 0; c < columnP; c++)
            {
                var obj = SpawnDefaultObject();
                SetPosition(obj, r, c);
                // var cell = obj.GetComponent<Cell>();
                // cell.SetData(new CellData()
                // {
                //     row = r,
                //     column = c,
                // });
                // // cell.SetDebug();
                // cell.AddTile(count);
                // cell.SetName();

                count++;
            }
        }
    }

    private void InitVarToSetPosition()
    {
        float maxHeight = (this.row - 1) * this.spacing;
        float maxWidth = (this.column - 1) * this.spacing;
        this.top = maxHeight / 2;
        this.left = maxWidth / 2;
    }

    private void SetPosition(Transform obj, int r, int c)
    {
        obj.transform.position = new Vector3(c * spacing - left, r * -spacing + top, 0);
    }

    protected override void OnTimeChange(int value)
    {
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        Spawn(4, 4);
    }

    public Tile GetNextTile(Tile tile)
    {
        // var maxIndexTile = this.tileCount - 1;
        // if (tile.Index == maxIndexTile) return null;
        // return Holder.Items.Find(item => item.GetComponent<Tile>().Index == tile.Index + 1).GetComponent<Tile>();
        return null;
    }
}