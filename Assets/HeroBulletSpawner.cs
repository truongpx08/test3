using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBulletSpawner : TruongSpawner
{
    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName(TruongConstants.Bullet);
    }
}