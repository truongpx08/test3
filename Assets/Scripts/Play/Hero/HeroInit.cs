using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInit : HeroRefAbstract
{
    [SerializeField] protected HeroData data;
    public HeroData Data => data;

    public void Init()
    {
        AddPosition();
        this.hero.AddName();
        this.hero.AddColor();
        AddHp();
        AddAtk();
        AddDurationAnim();
        AddIsInStatus();
    }

    private void AddPosition()
    {
        this.hero.transform.localPosition = Vector3.zero;
    }

    private void AddIsInStatus()
    {
        SetIsActive(false);
    }

    public void SetName(string value)
    {
        Debug.Log("Setting name");
        this.data.name = value;
    }

    public void SetColor(Color red)
    {
        this.hero.Model.color = red;
    }

    public void AddCurrentCell(Cell value)
    {
        this.data.currentCell = value;
    }

    private void AddAtk()
    {
        this.hero.AtkText.UpdateText(Random.Range(2, 4).ToString());
    }

    private void AddHp()
    {
        this.hero.HpText.UpdateText(Random.Range(10, 15).ToString());
    }

    private void AddDurationAnim()
    {
        this.data.durationAnim = 0.5f;
    }

    public void SetIsActive(bool value)
    {
        this.data.isActive = value;
    }
}