using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> panelList;

    private void Awake()
    {
        // Set Management
    }
    private IEnumerator Initialzing()
    {
        yield return new WaitUntil(() => Managers.Main.GameData != null);

        // 
    }
    private void 
}