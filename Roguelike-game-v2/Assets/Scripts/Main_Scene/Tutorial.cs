using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private List<List<GameObject>> panelList;
    private List<List<TextMeshProUGUI>> textList;

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
    private IEnumerator Initializing()
    {
        //yield return new WaitUntil(() => Managers.Main.stageDatas != null);

        yield return null;

        // == true

        PlayTutorial();
    }
    private IEnumerator PlayingTutorial()
    {
        for(int i = 0; i < 0; i++) // 0 <- count
        {
            //foreach() panel, text
            //{

            //}

            yield return null; // enter touch
        }

        // set tutorial value
    }
}