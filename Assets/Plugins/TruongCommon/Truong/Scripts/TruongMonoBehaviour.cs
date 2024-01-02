using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// Inheriting this class helps facilitate maintenance and expansion, enhancing flexibility in project development.   
/// </summary>
public abstract class TruongMonoBehaviour : MonoBehaviour
{
    /// <summary>
    /// Automatically set default values after code changes
    /// </summary>
    private async void OnValidate()
    {
        await Task.Delay(100); //Delay to wait for App.IsPlaying = true when entering PlayMode
        if (Application.isPlaying) return;
        if (!Application.isEditor) return;
        try
        {
            SetDefault();
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    protected virtual void Awake()
    {
        SetDefault();
    }


    protected virtual void OnEnable()
    {
        // For Override 
    }

    protected virtual void Start()
    {
        // For Override 
    }

    protected virtual void FixedUpdate()
    {
        // For Override 
    }

    protected virtual void Update()
    {
        // For Override 
    }

    protected virtual void LateUpdate()
    {
        // For Override 
    }

    protected virtual void OnDisable()
    {
        // For Override 
    }

    protected virtual void OnDestroy()
    {
        // For Override 
    }

    protected virtual void CreateChildren()
    {
    }

    /// <summary>
    /// Calling this function makes all variables and dependencies of self and children assigned values.
    /// </summary>
    [Button]
    protected void SetDefaultAll()
    {
        SetDefault();
        SetDefaultAllChild();
    }

    /// <summary>
    /// Renaming variables often leads to variables and dependencies being reset.
    /// Call this function in Awake to ensure that variables and dependencies are assigned values when entering the game.
    /// </summary>
    protected virtual void SetDefault()
    {
        CreateChildren();
        LoadComponents();
        SetVarToDefault();
    }

    /// <summary>
    /// Renaming variables often leads to variables being reset.
    /// Therefore, assign default values to variables in this function to initialize them quickly instead of re-entering them in the Unity editor.
    /// </summary>
    protected virtual void SetVarToDefault()
    {
        //For override
    }

    /// <summary>
    /// Renaming variables often leads to the loss of dependencies for components.
    /// Therefore, assign default values to components in this function to initialize them quickly instead of re-entering them in the Unity editor.
    /// </summary>
    protected virtual void LoadComponents()
    {
        //For override
    }


    protected void SwitchActiveChild(Action onSwitch)
    {
        var childList = GetAllChild();
        List<bool> childActiveList = new List<bool>();
        EnableAllChildren();
        onSwitch?.Invoke();
        ReturnToOldActiveOfAllChild();


        void EnableAllChildren()
        {
            childList.ForEach(item =>
            {
                childActiveList.Add(item.gameObject.activeSelf);
                item.gameObject.SetActive(true);
            });
        }


        void ReturnToOldActiveOfAllChild()
        {
            for (int i = 0; i < childList.Count; i++)
            {
                childList[i].gameObject.SetActive(childActiveList[i]);
            }
        }
    }

    /// <summary>
    /// Calling this function helps the children's variables and dependencies to be assigned values.
    /// </summary>
    protected void SetDefaultAllChild()
    {
        var child = GetComponentsInChildren<TruongMonoBehaviour>().ToList();
        child.ForEach(c => c.SetDefault());
    }


    protected T GetComponentInBro<T>()
    {
        return this.transform.parent.GetComponentInChildren<T>();
    }

    protected void CheckNull(object obj)
    {
        if (obj != null) return;
        Debug.LogError("object is null");
    }

    protected bool HasComponent<T>()
    {
        return GetComponent<T>() != null;
    }

    protected bool IsNull(object obj)
    {
        return obj == null;
    }

    protected GameObject GetChildWithName(string goName)
    {
        var list = GetAllChild();
        return list.Find(item => item.name == goName)?.gameObject;
    }


    protected List<Transform> GetAllChild()
    {
        var value = new List<Transform>();
        GetChild(this.transform, value);
        return value;

        void GetChild(Transform parent, List<Transform> list)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Transform child = parent.GetChild(i);
                list.Add(child);
                if (child.childCount > 0)
                    GetChild(child, list);
            }
        }
    }
}