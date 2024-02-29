using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public abstract class Cell : PlaySubscriber
{
    [TitleGroup("Data")]
    [SerializeField] private CellData data;
    public CellData Data => data;

    [TitleGroup("Ref")]
    [SerializeField] private CellPetSpawner petSpawner;
    public CellPetSpawner PetSpawner => petSpawner;
    [SerializeField] private CellPetDespawner petDespawner;
    public CellPetDespawner PetDespawner => petDespawner;

    [SerializeField] public SpriteRenderer model;

    public bool HasPet => petSpawner.Holder.Items.Any(h => h.gameObject.activeSelf);
    public Pet Pet => petSpawner.Holder.Items.Find(h => h.gameObject.activeSelf).GetComponent<Pet>();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadModel();
        LoadPetSpawner();
        LoadPetDespawner();
    }


    private void LoadModel()
    {
        this.model = transform.Find(TruongConstants.MODEL).GetComponent<SpriteRenderer>();
    }

    private void LoadPetDespawner()
    {
        this.petDespawner = GetComponentInChildren<CellPetDespawner>();
    }

    private void LoadPetSpawner()
    {
        this.petSpawner = GetComponentInChildren<CellPetSpawner>();
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
            $"Cell c{this.data.column}, r{this.data.row}, id{this.data.id}";
    }

    private void SetType(string value)
    {
        this.data.type = value;
    }

    public void SetBotNextCell(int value)
    {
        this.data.botNextCellId = value;
    }

    public void SetTopNextCell(int value)
    {
        this.data.topNextCellId = value;
    }

    protected void AddColor(Color value)
    {
        this.model.color = value;
    }

    public void AddType()
    {
        if (Data.row == 0)
        {
            SetType(CellType.TopReserve);
            return;
        }

        if (Data.row == CellSpawner.Row - 1)
        {
            SetType(CellType.BotReserve);
            return;
        }

        if (Data.row == 1 && Data.column == 0)
        {
            SetType(CellType.BotFinish);
            return;
        }

        if (Data.row == CellSpawner.Row - 2 && Data.column == 0)
        {
            SetType(CellType.TopFinish);
            return;
        }

        SetType(CellType.Combat);
    }

    public void SetBotPathId(int value)
    {
        this.Data.botPathId = value;
    }

    public void SetTopPathId(int value)
    {
        this.Data.topPathId = value;
    }
}