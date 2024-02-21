using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public abstract class TruongDespawner : TruongMonoBehaviour
{
    [SerializeField] private TruongSpawner spawner;

    public Action<Transform> onDespawn;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpawner();
    }

    private void LoadSpawner()
    {
        this.spawner = GetComponentInBro<TruongSpawner>();
    }

    [Button]
    public void DespawnObject(Transform obj)
    {
        if (obj == null) return;
        obj.gameObject.SetActive(false);
        if (obj.parent != this.spawner.Holder.transform)
            this.spawner.Holder.AddItem(obj);

        onDespawn?.Invoke(obj);
    }

    [Button]
    public void DespawnDefaultObject()
    {
        var prefab = spawner.Prefabs.GetDefaultPrefab();
        var item = spawner.Holder.Items.Find(i =>
            i.GetComponent<TruongId>().Id == prefab.GetInstanceID()
            && i.gameObject.activeSelf);
        DespawnObject(item);
    }

    [Button]
    public void DespawnAllObject()
    {
        spawner.Holder.Items.ForEach(DespawnObject);
    }
}