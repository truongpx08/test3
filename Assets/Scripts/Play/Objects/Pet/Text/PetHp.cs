using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class PetHp : PetRefTextAbstract
{
    public void ChangeValue(int value)
    {
        this.pet.Data.hp = Mathf.Clamp(value, 0, 50);
        UpdateText(this.pet.Data.hp.ToString());
    }
}