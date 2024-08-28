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
        BasicAttack_SO value = null;

        Task a = new Task(() => value = Util.LoadToPath<BasicAttack_SO>(Managers.Game.player.basicAttackTypeName).GetAwaiter().GetResult());

        a.Start();
        a.Wait();

        Debug.Log(value);

        //StartCoroutine(testCoroutine());

        //basicAttack = Util.Get<BasicAttack_SO>(Managers.Game.player.basicAttackTypeName);
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