using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayObjects : TruongSingleton<PlayObjects>
{
    [SerializeField] private CellSpawner cellSpawner;
    public CellSpawner CellSpawner => cellSpawner;
    [SerializeField] private HeroState heroState;
    public HeroState HeroState => heroState;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileSpawner();
        LoadHeroState();
    }

    private void LoadHeroState()
    {
        this.heroState = GetComponentInChildren<HeroState>();
    }

    private void LoadTileSpawner()
    {
        this.cellSpawner = GetComponentInChildren<CellSpawner>();
    }
}