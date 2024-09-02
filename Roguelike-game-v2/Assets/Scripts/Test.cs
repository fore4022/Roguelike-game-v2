using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public List<GameObject> objs;
    private void Start()
    {
        objs = EnemyDetection.FindLargestEnemyGroup(2);

        Debug.Log(objs.Count);

        foreach(GameObject go in objs)
        {
            Debug.Log(go.name);
        }
    }
}