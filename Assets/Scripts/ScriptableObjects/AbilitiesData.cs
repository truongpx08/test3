using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
using UnityEditor;

[CreateAssetMenu(fileName = "AbilitiesData", menuName = "ScriptableObjects/SpawnAbilitiesData", order = 1)]
public class AbilitiesData : ScriptableObject
{
    [SerializeField] private List<AbilityData> list;
    public List<AbilityData> List => list;

    [Serializable] public class AbilityData
    {
        public int id;
        public PetState.StateType triggerEvent;
        public string description;
    }

    [Button]
    public AbilityData GetDataWithId(int id)
    {
        return list.Find(item => item.id == id);
    }
}