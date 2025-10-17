using System.Collections.Generic;
using UnityEngine;
public static class TransformExtension
{
    public static List<T> GetComponentsInChild<T>(this Transform transform, bool recursive = false) where T : Component
    {
        List<T> components = new();

        if(recursive)
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                components.AddRange(transform.GetChild(index).GetComponentsInChild<T>(true));
            }

            if(transform.TryGetComponent(out T component))
            {
                components.Add(component);
            }
        }
        else
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                if(transform.GetChild(index).TryGetComponent(out T component))
                {
                    components.Add(component);
                }
            }
        }

        return components;
    }
    public static T GetComponentInChild<T>(this Transform transform, bool recursive = false) where T : Component
    {
        if(recursive)
        {
            return transform.GetComponentInChildren<T>();
        }
        else
        {
            for(int index = 0; index < transform.childCount; index++)
            {
                if(transform.GetChild(index).TryGetComponent(out T component))
                {
                    return component;
                }
            }
        }

        return default;
    }
}