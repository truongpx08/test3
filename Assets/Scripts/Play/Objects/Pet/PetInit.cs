using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInit : PetRefAbstract
{
    public void Init()
    {
        AddPosition();
        this.pet.AddType();
        this.pet.AddColor();
        this.pet.AddFinishCellType();
        AddHp();
        AddAtk();
        AddDurationAnim();
        AddIsInStatus();
    }

    private void AddPosition()
    {
        this.pet.transform.localPosition = Vector3.zero;
    }

    private void AddIsInStatus()
    {
        SetIsActive(false);
    }

    public void SetType(string value)
    {
        Debug.Log("Setting name");
        this.Data.type = value;
    }

    public void SetColor(Color red)
    {
        this.pet.Model.color = red;
    }

    public void AddCurrentCell(Cell value)
    {
        this.Data.currentCell = value;
    }

    private void AddAtk()
    {
        this.pet.AtkText.UpdateText(Random.Range(2, 4).ToString());
    }

    private void AddHp()
    {
        this.pet.HpText.UpdateText(Random.Range(10, 15).ToString());
    }

    private void AddDurationAnim()
    {
        this.Data.durationAnim = 0.5f;
    }

    public void SetIsActive(bool value)
    {
        this.Data.isActive = value;
    }

    public void AddId(int value)
    {
        this.Data.id = value;
    }

    public void AddIsBoss(bool value)
    {
        this.Data.isBoss = value;
    }
}