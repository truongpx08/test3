using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HeroText : TMPTextHandler
{
    [SerializeField] protected Hero hero;

    private void LoadHeroRef()
    {
        this.hero = GetComponentInParent<Hero>();
    }

    public override void UpdateText(string value)
    {
        base.UpdateText(value);
        LoadHeroRef();
        SaveData();
    }

    protected abstract void SaveData();

}