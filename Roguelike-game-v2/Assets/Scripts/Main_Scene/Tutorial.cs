using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    [Header("Step_1")]
    [SerializeField]
    private List<GameObject> panelList_1;
    private List<TextMeshProUGUI> textList_1;

    private float step_Index = 0;
            
    private void Awake()
    {
        // if user data == null - init

        StartCoroutine(Initializing());
    }
    public void PlayTutorial()
    {
        step_Index = 0;

        StartCoroutine(PlayingTutorial());
    }
    private void Step_1()// Enable
    {
        foreach(GameObject panel in panelList_1)
        {
            panel.SetActive(true);
        }
    }

    //Disable
    private IEnumerator Initializing()
    {
        yield return new WaitUntil(() => Managers.Main.StageDatas != null);

        // == true

        PlayTutorial();
    }
    private IEnumerator PlayingTutorial()
    {
        Step_1();

        yield return null; // enter touch

        //Step_2();

        //yield return null;

        //Step_3();

        //yield return null;

        //Step_4();

        //yield return null;

        //Step_5();

        //yield return null;
    }
}