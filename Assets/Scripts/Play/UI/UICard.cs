using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class UICard : PlaySubscriber
{
    [Serializable] public class CardData
    {
        public int id;
        public int petId;
        public string type;
    }

    [SerializeField] private Button button;
    [SerializeField] private CardData cardData;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadButton();
    }

    protected override void Start()
    {
        base.Start();
        this.button.onClick.AddListener(OnClickButton);
        this.cardData = new CardData
        {
            id = 1,
            petId = 2,
            type = PetType.Bot,
        };
    }

    private void LoadButton()
    {
        this.button = transform.Find(TruongConstants.Button).GetComponent<Button>();
    }

    private void OnClickButton()
    {
        UseBotCard();
    }

    [Button]
    void UseBotCard()
    {
        var reserveCells = PlayObjects.Instance.CellSpawner.ReserveBotCells;
        Use(reserveCells);
    }

    void Use(List<Cell> reserveCells)
    {
        Cell cell = GetCellToSummon(reserveCells);
        SummonPet(cell);
    }

    private Cell GetCellToSummon(List<Cell> reserveCells)
    {
        for (int i = 0; i < reserveCells.Count; i++)
        {
            var cell = reserveCells[i];
            if (i == 0)
            {
                if (cell.HasPet) break;
            }

            if (i == reserveCells.Count - 1)
            {
                if (!cell.HasPet)
                {
                    return cell;
                }
            }

            if (!cell.HasPet) continue;
            var previousCell = reserveCells[i - 1];
            return previousCell;
        }

        return null;
    }

    private void SummonPet(Cell cellToSummon)
    {
        if (cellToSummon == null) return;

        Pet pet = cellToSummon.PetSpawner.SpawnPet();
        pet.AddDataWithPetId(this.cardData.petId);
        pet.AddAbility();
        pet.Init.SetType(this.cardData.type);
        pet.Init.Init();
        pet.Init.SetCurrentCell(cellToSummon);

        PetReference.Instance.Pets.Add(pet);
    }
}