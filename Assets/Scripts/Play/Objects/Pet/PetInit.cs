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
        switch (this.Data.type)
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
        this.Data.type = value;
    }

    public void SetColor(Color red)
    {
        this.pet.Model.color = red;
    }

    public void SetCurrentCell(Cell value)
    {
        this.Data.currentCell = value;
    }

    private void AddAtk()
    {
        this.pet.AtkText.UpdateText(this.Data.atk.ToString());
    }

    private void AddHp()
    {
        this.pet.HpText.UpdateText(this.Data.hp.ToString());
    }

    private void AddDurationAnim()
    {
        this.Data.durationAnim = 0.5f;
    }

    public void SetIsActive(bool value)
    {
        this.Data.isActive = value;
    }

    public void AddIsBoss(bool value)
    {
        this.Data.isBoss = value;
    }

    private void AddFinishCellType()
    {
        switch (this.Data.type)
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
        this.Data.finishCellType = value;
    }
}