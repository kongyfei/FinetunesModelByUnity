using UnityEngine;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public static class UnityExtension
{ 
    public static void SetActiveAvoidNullError(this GameObject gameObject, bool isActive)
    {
        if (gameObject && (gameObject.activeSelf ^ isActive))
        {
            gameObject.SetActive(isActive);
        }
    }


    public static T GetComponentAndCreateIfNonExist<T>(this GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null)
        {
            component = gameObject.AddComponent<T>();
        }
        return component;
    }
    public static T AddComponentOnly<T>(this GameObject target) where T : Component
    {
        T result = null;

        if (target != null)
        {
            var arr = target.GetComponentsInChildren<T>(true);
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    GameObject.DestroyImmediate(arr[i], true);
                }
            }
            result = target.AddComponent<T>();
        }

        return result;
    }
    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    public static void SetPositionX(this Transform transform, float x)
    {
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    public static void SetPositionY(this Transform transform, float y)
    {
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    public static void SetPositionZ(this Transform transform, float z)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, z);
    }

    public static void SetLocalPositionX(this Transform transform, float x)
    {
        transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
    }

    public static void SetLocalPositionY(this Transform transform, float y)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
    }

    public static void SetLocalPositionZ(this Transform transform, float z)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
    }

    public static bool IsBumping(this CollisionFlags collisionFlags)
    {
        return (collisionFlags & (CollisionFlags.CollidedSides)) != 0;
    }

    public static bool IsGrounded(this CollisionFlags collisionFlags)
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }

    public static bool IsHittingHead(this CollisionFlags collisionFlags)
    {
        return (collisionFlags & CollisionFlags.CollidedAbove) != 0;
    }

    public static void DestroyBasedOnRunning(this UnityEngine.Object anObject)
    {
        if (Application.isPlaying)
        {
            GameObject.Destroy(anObject);
        }
        else
        {
            GameObject.DestroyImmediate(anObject);
        }
    }

    public static bool ExistChildByName(this Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name.Trim().Equals(name.Trim()))
            {
                return true;
            }
        }
        return false;
    }

    public static Transform GetChildByName(this Transform parent, string name)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (parent.GetChild(i).name.Trim().Equals(name.Trim()))
            {
                return parent.GetChild(i);
            }
        }
        return null;
    }

    public static Transform GetChildRecursionByName(this Transform parent, string name, bool fullSearch)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            if (fullSearch ?
                    parent.GetChild(i).name.Trim().Equals(name) :
                    parent.GetChild(i).name.Trim().Contains(name))
            {
                return parent.GetChild(i);
            }
        }

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform result = GetChildRecursionByName(parent.GetChild(i), name, fullSearch);

            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    public static void DebugChildRecursion(this Transform trans)
    {
        Debug.LogWarning(trans.name, trans.gameObject);
        for (int i = 0; i < trans.childCount; i++)
        {
            trans.GetChild(i).DebugChildRecursion();
        }
        Debug.LogWarning("-----------");
    }

    public static Material GetMaterial(this Renderer renderer)
    {
#if UNITY_EDITOR
        return renderer.material;
#else
        return renderer.sharedMaterial;
#endif
    }

    public static Material[] GetMaterials(this Renderer renderer)
    {
#if UNITY_EDITOR
        return renderer.materials;
#else
        return renderer.sharedMaterials;
#endif
    }

    public static void SetMaterial(this Renderer renderer, Material mat)
    {
#if UNITY_EDITOR
        renderer.material = mat; ;
#else
        renderer.sharedMaterial = mat;
#endif
    }

    public static void CreateChild<T>(this Transform parent, GameObject clonePrefab
                                      , System.Collections.Generic.List<T> dataList, System.Action<int, GameObject, T> OnCreateCallBack, bool isClear, bool isResetTf = true)
    {
        if (clonePrefab == null)
        {
            if (parent.childCount > 0)
            {
                clonePrefab = parent.GetChild(0).gameObject;
            }
            else
            {
                return;
            }
        }
        int needCount = dataList.Count;
        int factCount = parent.childCount;
        for (int i = 0, len = Mathf.Max(needCount, factCount); i < len; i++)
        {
            GameObject tempGO = null;

            if (i < needCount)
            {
                if (i < factCount)
                {
                    tempGO = parent.GetChild(i).gameObject;
                }
                else
                {
                    tempGO = GameObject.Instantiate(clonePrefab);
                    tempGO.transform.SetParent(parent);
                }
                if (isResetTf)
                {
                    tempGO.transform.Reset();
                }
                tempGO.SetActive(true);
                OnCreateCallBack(i, tempGO, dataList[i]);
            }
            else
            {
                if (isClear)
                {
                    GameObject.Destroy(parent.GetChild(i));
                }
                else
                {
                    parent.GetChild(i).gameObject.SetActiveAvoidNullError(false);
                }
            }
        }
    }
}
