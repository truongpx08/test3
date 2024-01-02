using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Sirenix.OdinInspector;

/// <summary>
/// Inherit this class to save and load data
/// </summary>
public abstract class TruongEasySave<T> : TruongSingleton<T>
{
    [SerializeField] private string dataPath;
    [SerializeField] private TruongGameData gameData;
    public TruongGameData GameData => gameData;

    protected override void SetVarToDefault()
    {
        base.SetVarToDefault();
        dataPath = Path.Combine(Application.persistentDataPath, TruongConstants.FILE_NAME);
    }

    protected override void Awake()
    {
        base.Awake();
        Debug.Log("Data path \n " + dataPath);
        TryLoadLocalData();
    }

    private void TryLoadLocalData()
    {
        if (!File.Exists(dataPath))
        {
            Debug.Log("Init data");
            Initialize();
            return;
        }

        try
        {
            SetGameData();
            OnDataLoaded();
            Debug.Log("Load data successfully");
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            ResetData();
        }
    }

    private void Initialize()
    {
        ResetData();
    }

    private void SetGameData()
    {
        string data = GetDataJson();
        string decrypted = TruongUtils.XOROperator(data, TruongConstants.KEY);
        gameData = JsonUtility.FromJson<TruongGameData>(decrypted);
    }

    protected abstract void OnDataLoaded();

    private void ResetData()
    {
        SetDefaultGameData();
        SaveData();
        OnDataLoaded();
    }

    protected abstract void SetDefaultGameData();

    protected virtual void SetDefaultGameData(TruongGameData value)
    {
        this.gameData = value;
    }

    [Button]
    public void SaveData()
    {
        string origin = JsonUtility.ToJson(gameData);
        string encrypted = TruongUtils.XOROperator(origin, TruongConstants.KEY);
        File.WriteAllText(dataPath, encrypted);
        Debug.Log("Save data \n " + dataPath);
    }

    private string GetDataJson()
    {
        return File.ReadAllText(dataPath);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    // protected override void SetDontDestroyOnLoad()
    // {
    //     SetDontDestroyOnLoad(true);
    // }
}