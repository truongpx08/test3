using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : TruongSingleton<User>
{
    [SerializeField] private TileSpawner tileSpawner;
    public TileSpawner TileSpawner => tileSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileSpawner();
    }

    private void LoadTileSpawner()
    {
        this.tileSpawner = GetComponentInChildren<TileSpawner>();
    }
}