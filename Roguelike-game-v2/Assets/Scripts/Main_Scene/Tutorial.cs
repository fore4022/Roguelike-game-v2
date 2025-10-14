using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    [Header("Step_1")]
    [SerializeField]
    private List<TextMeshProUGUI> textList;
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Image maskImage;

    private const float duration = 0.2f;
    private const float targetAlpha = 165;

    private bool nnn = false;
    private int stepCount = 0;

    private void Start()
    {
        StartCoroutine(Initializing());
    }
    private void Step_1() // ? courotine
    {
        nnn = false;

        //

        nnn = true;
    }
    private void Step_2()
    {

    }
    private void Step_3()
    {

    }
    private IEnumerator Initializing()
    {
        yield return new WaitUntil(() => Managers.Data.data != null);

        if(!Managers.Data.data.Tutorial)
        {
            StartCoroutine(PlayingTutorial());
        }
    }
    private IEnumerator PlayingTutorial()
    {
        UIElementUtility.SetImageAlpha(maskImage, targetAlpha, duration);

        yield return new WaitForSeconds(duration);

        Step_1();

        while(true)
        {
            switch(stepCount)
            {
                case 0:
                    Step_1();
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }

            yield return null;
        }

        //maskImage.gameObject.SetActive(false);

        //Managers.Data.data.Tutorial = true;
    }
}