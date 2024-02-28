using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class HpText : PetText
{
    protected override void SaveData()
    {
        this.pet.Data.hp = int.Parse(text);
    }
}