using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayData : TruongSingleton<PlayData>
{
    [SerializeField] private PetsData petsData;
    public PetsData PetsData => petsData;
}