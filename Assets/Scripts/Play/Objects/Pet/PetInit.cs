using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInit : PetRefAbstract
{
    public void Init()
    {
        AddPosition();
        AddIsActive();
        AddColor();
        AddFinishCellType();
        AddDurationAnim();
        AddAtk();
        AddHp();
    }

    private void AddColor()
    {
        switch (this.PetData.type)
        {
            case PetType.Bot:
                SetColor(Color.blue);
                break;

            case PetType.Top:
                SetColor(Color.red);
                break;
        }
    }

    private void AddPosition()
    {
        this.pet.transform.localPosition = Vector3.zero;
    }

    private void AddIsActive()
    {
        SetIsActive(false);
    }

    public void SetType(string value)
    {
        this.PetData.type = value;
    }

    public void SetColor(Color red)
    {
        this.pet.Model.color = red;
    }

    public void SetCurrentCell(Cell value)
    {
        this.PetData.currentCell = value;
    }

    private void AddAtk()
    {
        this.pet.Atk.UpdateText(this.PetData.atk.ToString());
    }

    private void AddHp()
    {
        this.pet.Hp.UpdateText(this.PetData.hp.ToString());
    }

    private void AddDurationAnim()
    {
        this.PetData.durationAnim = 0.5f;
    }

    public void SetIsActive(bool value)
    {
        // Debug.Log($"debug {this.PetData.id} {value}");
        this.PetData.isActive = value;
    }

    public void AddIsBoss(bool value)
    {
        this.PetData.isBoss = value;
    }

    private void AddFinishCellType()
    {
        switch (this.PetData.type)
        {
            case PetType.Bot:
                SetFinishCellType(CellType.BotFinish);
                break;

            case PetType.Top:
                SetFinishCellType(CellType.TopFinish);
                break;
        }
    }

    private void SetFinishCellType(string value)
    {
        this.PetData.finishCellType = value;
    }
}