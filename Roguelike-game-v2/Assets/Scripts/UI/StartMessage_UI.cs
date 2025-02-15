using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : UserInterface
{
    private TextMeshProUGUI tmp;

    private Coroutine textAnimation;
    private WaitForSeconds delay;
    private const float duration = 1.5f;
    private const int minAlpha = 50;
    private const int maxAlpha = 255;

    public override void SetUserInterface()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    public void IsLoading(bool isLoading)
    {
        if(isLoading)
        {
            textAnimation = StartCoroutine(Loading());  
        }
        else
        {
            StartCoroutine(Blink());
        }
    }
    private IEnumerator Loading()
    {
        string text = "Loading";
        int count = 0;
        int i;

        delay = new(duration / 2);

        while (true)
        {
            tmp.text = text;

            for(i = 0; i < count; i++)
            {
                tmp.text += ".";
            }

            yield return delay;

            count++;

            if(count > 3)
            {
                count = 0;
            }
        }
    }
    private IEnumerator Blink()
    {
        StopCoroutine(textAnimation);

        delay = new(duration);

        tmp.text = "PRESS TO START";

        while (true)
        {
            Managers.UI.uiElementUtility.SetTextAlpha(tmp, minAlpha, duration, false);

            yield return delay;

            Managers.UI.uiElementUtility.SetTextAlpha(tmp, maxAlpha, duration, false);

            yield return delay;
        }
    }
}