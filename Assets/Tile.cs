using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Tile : SpawnerObj
{
    public const string Normal = "Normal";
    public const string Portal = "Portal";
    public const string Combat = "Combat";
    [SerializeField] private int index;
    public int Index => index;
    [SerializeField] private string type;

    public void SetIndex(int value)
    {
        this.index = value;
        SetType();
    }

    private void SetType()
    {
        switch (this.index)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                this.type = Normal;
                break;
            case 6:
                this.type = Portal;
                break;

            case 7:
                this.type = Combat;
                break;
        }

        SetPosition();
    }

    private void SetPosition()
    {
        var thisTrans = transform;
        if (index >= 7)
        {
            thisTrans.position = new Vector3(0.379999995f, 0.769999981f, 0);
            return;
        }

        var a = new Vector3(-2, 0, 0);
        var b = new Vector3(2, 0, 0);
        var ab = Vector3.Distance(a, b);
        thisTrans.position = new Vector3(b.x -((index / 6f) * ab) , thisTrans.position.y);
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
}