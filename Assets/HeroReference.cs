using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroReference : TruongSingleton<HeroReference>
{
    public List<Hero> heroes;
}