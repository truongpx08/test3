using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class HpText : HeroText
{
    protected override void SaveData()
    {
        this.hero.Data.hp = int.Parse(text);
    }
}