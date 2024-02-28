using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UICard : PlaySubscriber
{
    [SerializeField] private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    protected override void Start()
    {
        base.Start();
        this.button.onClick.AddListener(OnClickButton); 
    }

    private void LoadButton()
    {
        this.button = transform.Find(TruongConstants.Button).GetComponent<Button>();
    }

    private void OnClickButton()
    {
        UseAlly();
    }

    [Button]
    void UseEnemy()
    {
        var reserveCells = PlayObjects.Instance.CellSpawner.ReserveEnemyCells;
        Use(PetType.Enemy, reserveCells);
    }

    [Button]
    void UseAlly()
    {
        var reserveCells = PlayObjects.Instance.CellSpawner.ReserveAllyCells;
        Use(PetType.Ally, reserveCells);
    }

    void Use(string heroType, List<Cell> reserveCells)
    {
        for (int i = 0; i < reserveCells.Count; i++)
        {
            var cell = reserveCells[i];
            if (i == 0)
            {
                if (cell.HasHero) break;
            }

            if (i == reserveCells.Count - 1)
            {
                if (!cell.HasHero)
                {
                    SpawnPetWithType(heroType, cell);
                    break;
                }
            }

            if (!cell.HasHero) continue;
            var previousCell = reserveCells[i - 1];
            SpawnPetWithType(heroType, previousCell);
            break;
        }
    }

    private void SpawnPetWithType(string heroType, Cell spawnOnCell)
    {
        switch (heroType)
        {
            case PetType.Ally:
                spawnOnCell.PetSpawner.SpawnAlly();
                break;
            case PetType.Enemy:
                spawnOnCell.PetSpawner.SpawnEnemy();
                break;
        }
    }
}