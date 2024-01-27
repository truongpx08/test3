using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TileSpawner : SpawnerObj
{
    private int tileCount;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        tileCount = 8;
    }

    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName("HeroHolder");
    }

    [Button]
    private void SpawnHeroHolder()
    {
        for (int i = 0; i < this.tileCount; i++)
        {
            var go = SpawnDefaultObject();
            go.GetComponent<Tile>().SetIndex(i);
        }
    }

    protected override void OnTimeChange(int value)
    {
    }

    protected override void OnStateChange(string value)
    {
        if (value != GameState.OnStart) return;
        SpawnHeroHolder();
    }

    public Tile GetNextTile(Tile tile)
    {
        var maxIndexTile = this.tileCount - 1;
        if (tile.Index == maxIndexTile) return null;
        return Holder.Items.Find(item => item.GetComponent<Tile>().Index == tile.Index + 1).GetComponent<Tile>();
    }
}