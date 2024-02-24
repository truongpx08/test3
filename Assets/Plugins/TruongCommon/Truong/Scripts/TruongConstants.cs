using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TruongConstants
{
    //Spawner
    public const string PREFABS = "Prefabs";
    public const string HOLDER = "Holder";

    //EasySave
    public const string KEY = "KamperTee";
    public const string FILE_NAME = "data.JSON";

    //UI
    public const string BG = "Bg";
    public const string CONTENT = "Content";
    public const string TITLE = "Title";
    public const string QUIT_BUTTON = "QuitButton";
    public const string CLOSE_BUTTON = "CloseButton";

    //Audio
    public const string BGM = "BGM";
    public const string SFX = "SFX";
    public const string AUDIO_CLIPS = "AudioClips";

    //Object
    public const string MODEL = "Model";
    public const string COMMON_GO_SPAWNER = "CommonGoSpawner";
    public const string Bullet = "Bullet";
    public const string Path = "Path";
    public const string PathHorizontal = "PathHorizontal";
    public const string PathVertical = "PathVertical";
}

[System.Serializable]
public class ResourcesFolderName
{
    public const string Pvp = "Pvp";
    public const string Tile = "Tile";
}