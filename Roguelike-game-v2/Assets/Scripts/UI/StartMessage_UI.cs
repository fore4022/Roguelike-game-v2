using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : UserInterface
{
    private TextMeshProUGUI tmp;

    private Coroutine textAnimation;
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
        string text = "Loading.";
        int count = 0;
        int i;

        while (true)
        {
            tmp.text = text;

            for(i = 0; i < count; i++)
            {
                tmp.text += ".";
            }

            yield return new WaitForSeconds(duration);

            count++;

            if(count > 2)
            {
                count = 0;
            }
        }
    }
    private IEnumerator Blink()
    {
        StopCoroutine(textAnimation);

        tmp.text = "PRESS TO START";

        while (true)
        {
            Managers.UI.uiElementUtility.SetTextAlpha(tmp, minAlpha, duration, false);

            yield return new WaitForSecondsRealtime(duration);

            Managers.UI.uiElementUtility.SetTextAlpha(tmp, maxAlpha, duration, false);

            yield return new WaitForSecondsRealtime(duration);
        }
    }
}