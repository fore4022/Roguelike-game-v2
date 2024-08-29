using JetBrains.Annotations;
using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    BasicAttack_SO basicAttack = null;

    private void Start()
    {

    }
    private IEnumerator testCoroutine()
    {
        while (true)
        {
            if(basicAttack != null)
            {
                Debug.Log(basicAttack);
            }

            yield return null;
        }
    }
}