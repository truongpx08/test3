using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "AbilitiesData", menuName = "ScriptableObjects/SpawnAbilitiesData", order = 1)]
public class AbilitiesData : ScriptableObject
{
    [SerializeField] private List<Data> list;
    public List<Data> List => list;

    [Serializable] public class Data
    {
        public int id;
        public PetState.StateType triggerEvent;
        public string description;
    }

    [Button]
    public Data GetDataWithId(int id)
    {
        return list.Find(item => item.id == id);
    }
}