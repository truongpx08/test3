using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TruongChild : TruongMonoBehaviour
{
    protected GameObject CreateChild(string objName)
    {
        if (string.IsNullOrEmpty(objName)) return null;
        if (transform.Find(objName) != null) return null;

        var newGo = new GameObject
        {
            name = objName,
            transform =
            {
                parent = this.transform
            }
        };

        ResetTransformObj(newGo);
        return newGo.gameObject;
    }

    protected virtual void ResetTransformObj(GameObject obj)
    {
        ResetTransformObj(obj.transform);
    }

    protected virtual void ResetTransformObj(Transform obj)
    {
        obj.localPosition = Vector3.zero;
        obj.localRotation = Quaternion.identity;
        obj.localScale = Vector3.one;
    }
}