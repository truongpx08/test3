using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayObjects : TruongSingleton<PlayObjects>
{
    [SerializeField] private CellSpawner cellSpawner;
    public CellSpawner CellSpawner => cellSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileSpawner();
    }

    private void LoadTileSpawner()
    {
        this.cellSpawner = GetComponentInChildren<CellSpawner>();
    }
}