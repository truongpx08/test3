using Sirenix.OdinInspector;
using UnityEngine;

public abstract class TruongSpawner : TruongChild
{
    [TitleGroup("Spawner")]
    [SerializeField] private TruongHolder holder;
    public TruongHolder Holder => holder;
    [SerializeField] private TruongPrefabs prefabs;
    public TruongPrefabs Prefabs => prefabs;

    protected override void CreateChildren()
    {
        base.CreateChildren();
        CreateChild(TruongConstants.HOLDER)?.AddComponent<TruongHolder>();
        CreateChild(TruongConstants.PREFABS)?.AddComponent<TruongPrefabs>();
    }

    protected override void LoadComponents()
    {
        LoadPrefabsFolder();
        LoadContainerFolder();
        LoadPrefabInResource();
    }

    private void LoadContainerFolder()
    {
        holder = GetComponentInChildren<TruongHolder>();
    }

    private void LoadPrefabsFolder()
    {
        prefabs = GetComponentInChildren<TruongPrefabs>();
    }

    protected abstract void LoadPrefabInResource();

    protected void LoadPrefabInResourceWithPrefabName(string prefabName)
    {
        prefabs.LoadPrefabWithName(prefabName);
    }

    protected void LoadPrefabInResourceWithPrefabPath(string prefabPath)
    {
        prefabs.LoadPrefabWithPath(prefabPath);
    }

    protected void SpawnAllPrefab()
    {
        this.Prefabs.Items.ForEach(item => SpawnObjectWithPrefab(item));
    }

    [Button]
    protected Transform SpawnObject(string prefabName)
    {
        var prefab = prefabs.GetPrefabWithName(prefabName);
        return SpawnObjectWithPrefab(prefab);
    }

    [Button]
    public Transform SpawnDefaultObject()
    {
        var prefab = prefabs.GetDefaultPrefab();
        return SpawnObjectWithPrefab(prefab);
    }

    protected Transform SpawnObjectWithPrefab(Transform prefab)
    {
        if (prefab == null)
        {
            Debug.LogError($"No prefab found");
            return null;
        }

        var result = Holder.GetAvailableItemForReuse(prefab);
        return result == null ? InstantiateNewObject(prefab) : result;
    }

    private Transform InstantiateNewObject(Transform prefab)
    {
        Transform newObj = Instantiate(prefab);
        TruongUtils.SetNameObject(newObj, prefab.name);
        AddItemToHolder(newObj);
        ResetTransformObj(newObj);
        TruongUtils.AddIdToObject(prefab.GetInstanceID(), newObj);
        return newObj;
    }

    private void AddItemToHolder(Transform newObj)
    {
        holder.AddItem(newObj);
    }
}