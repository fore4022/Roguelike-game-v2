using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    [Header("Step_1")]
    [SerializeField]
    private GameObject panel;
    private TextMeshProUGUI text;
            
    private void Awake()
    {
        // Set Management
    }
    private IEnumerator Initialzing()
    {
        yield return new WaitUntil(() => Managers.Main.StageDatas != null);

        
    }
    private void Step_1()
    {

    }
    private void Step_2()
    {

    }
    private void Step_3()
    {

    }
    private void Step_4()
    {

    }
    private void Step_5()
    {

    }
}