using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TruongCreateFolders : MonoBehaviour
{
    [MenuItem("Truong/Create 2D Folders")]
    private static void Create2DFolders()
    {
        List<string> list = new List<string>
        {
            TruongFolderName.SCENES,
            TruongFolderName.SCRIPTS,
            TruongFolderName.PREFABS,
            TruongFolderName.RESOURCES,
            TruongFolderName.PLUGINS,
            TruongFolderName.SPRITES,
        };
        list.ForEach(item => { CreateAFolder(TruongFolderName.ASSETS, item); });

        CreateFolderInResources(TruongFolderName.PREFABS);
        CreateFolderInResources(TruongFolderName.SPRITES);
        CreateAFolder(Path.Combine(TruongFolderName.ASSETS, TruongFolderName.RESOURCES, TruongFolderName.PREFABS),
            TruongFolderName.LOAD_ON_LOAD_SCENE);
    }

    private static void CreateFolderInResources(string folderName)
    {
        string resourcesPath = Path.Combine(TruongFolderName.ASSETS, TruongFolderName.RESOURCES);
        CreateAFolder(resourcesPath, folderName);
    }

    private static void CreateAFolder(string parentFolder, string folderName)
    {
        string prefabFolderPath = Path.Combine(parentFolder, folderName);
        if (AssetDatabase.IsValidFolder(prefabFolderPath)) return;

        AssetDatabase.CreateFolder(parentFolder, folderName);
        Log(folderName);
    }

    private static bool HasFolder(string folderName)
    {
        string folderPath = Path.Combine(Application.dataPath, folderName);
        return Directory.Exists(folderPath);
    }

    private static void Log(string item)
    {
        Debug.Log("Has created folder " + item);
    }
}