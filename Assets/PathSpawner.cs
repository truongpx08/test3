using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : TruongSpawner
{
    protected override void LoadPrefabInResource()
    {
        LoadPrefabInResourceWithPrefabName(TruongConstants.Path);
    }

    public GameObject SpawnHorPath()
    {
        return SpawnObjectWithName(TruongConstants.PathHorizontal).gameObject;
    }

    public GameObject SpawnVerticalPath()
    {
        return SpawnObjectWithName(TruongConstants.PathVertical).gameObject;
    }
}