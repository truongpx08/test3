using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class TruongGameObject : TruongMonoBehaviour
{
    [Button]
    public virtual void Enable()
    {
        // Debug.Log("Enable "+ this.name);
        this.transform.gameObject.SetActive(true);
    }

    [Button]
    public virtual void Disable()
    {
        // Debug.Log("Disable "+ this.name);
        this.transform.gameObject.SetActive(false);
    }

    public void EnableGo(Transform go)
    {
        go.gameObject.SetActive(true);
    }

    public void DisableGo(Transform go)
    {
        go.gameObject.SetActive(false);
    }
}