using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayObjects : TruongSingleton<PlayObjects>
{
    [SerializeField] private CellSpawner cellSpawner;
    public CellSpawner CellSpawner => cellSpawner;
    [SerializeField] private HeroState heroState;
    public HeroState HeroState => heroState;

    [SerializeField] private Time time;
    public Time Time => time;
    [SerializeField] private PathSpawner pathSpawner;
    public PathSpawner PathSpawner => pathSpawner;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTileSpawner();
        LoadHeroState();
        LoadTime();
        LoadPathSpawner();
    }

    private void LoadPathSpawner()
    {
        this.pathSpawner = GetComponentInChildren<PathSpawner>();
    }

    private void LoadTime()
    {
        this.time = GetComponentInChildren<Time>();
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