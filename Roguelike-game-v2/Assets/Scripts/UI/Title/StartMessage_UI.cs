using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : UserInterface
{
    private TextMeshProUGUI tmp;

    private const float duration = 1.2f;

    private Coroutine textAnimation;
    private Coroutine blink;
    private WaitForSeconds delay;
    private int state = 0;

    public override void SetUserInterface()
    {
        tmp = GetComponent<TextMeshProUGUI>();

        textAnimation = StartCoroutine(Effecting());
    }
    public void SetState()
    {
        state++;

        if(state == 1)
        {
            Blink();
        }
        else if(state == 2)
        {
            StopCoroutine(textAnimation);

            textAnimation = StartCoroutine(Effecting());
        }
    }
    private IEnumerator Effecting()
    {
        string text;
        int count = 0;
        int i;

        if(state == 0)
        {
            text = "Loading";
        }
        else
        {
            text = "Starting";

            StopCoroutine(blink);
            UIElementUtility.SetTextAlpha(tmp, 255, 0);
        }

        delay = new(duration / 2);

        while(true)
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
    private void Blink()
    {
        StopCoroutine(textAnimation);

        tmp.text = "PRESS TO START";
        blink = StartCoroutine(UIElementUtility.BlinkText(tmp, duration, false));
    }
}