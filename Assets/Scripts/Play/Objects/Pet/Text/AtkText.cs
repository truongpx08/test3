using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkText : PetText
{
    protected override void SaveData()
    {
        this.pet.Data.atk = int.Parse(text);
    }
}