using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkText : HeroText
{
    protected override void SaveData()
    {
        this.hero.Init.Data.atk = int.Parse(text);
    }
}