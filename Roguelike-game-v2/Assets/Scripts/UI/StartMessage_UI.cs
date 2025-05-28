using System.Collections;
using TMPro;
using UnityEngine;
public class StartMessage_UI : UserInterface
{
    private TextMeshProUGUI tmp;

    private const float duration = 1.5f;

    private Coroutine textAnimation;
    private Coroutine blink;
    private WaitForSeconds delay;
    private int state = 0;

    public override void SetUserInterface()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetState()
    {
        if(state != 1)
        {
            textAnimation = StartCoroutine(Effecting());
        }
        else
        {
            Blink();
        }

        state++;
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

        delay = new(duration);

        tmp.text = "PRESS TO START";
        blink = StartCoroutine(UIElementUtility.BlinkText(tmp, duration, false));
    }
}