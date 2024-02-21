using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRefAbstract : TruongMonoBehaviour
{
    [SerializeField] protected Hero hero;
    protected HeroData data => this.hero.Init.Data;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHeroRef();
    }

    private void LoadHeroRef()
    {
        this.hero = GetComponentInParent<Hero>();
    }
}