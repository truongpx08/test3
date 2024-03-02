using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(fileName = "PetsData", menuName = "ScriptableObjects/SpawnPetsData", order = 1)]
public class PetsData : ScriptableObject
{
    [SerializeField] private List<PetData> list;
    public List<PetData> List => list;

    [Serializable] public class PetData
    {
        public int id;
        public Sprite icon;
        public PetAbility ability;
        public int hp;
        public int atk;
    }

    [Button]
    public PetData GetPetDataWithId(int id)
    {
        return list.Find(item => item.id == id);
    }
}