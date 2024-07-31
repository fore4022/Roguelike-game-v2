using System.Collections.Generic;
using UnityEngine;
public class ObjectPool
{
    //public static Dictionary<string, >
    public static GameObject root;

    public static void Init()
    {
        GameObject go = GameObject.Find("@ObjectPool");

        if(go == null)
        {
            root = go = new GameObject { name = "@ObjectPool" };
        }
    }
    public static void CreateObjects()
    {

    }
    public static GameObject ActiveObject()
    {
        return null;
    }
    public static void DisableObject()
    {

    }
}