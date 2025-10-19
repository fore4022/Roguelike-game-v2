using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
public static class Typing
{
    private static WaitForSecondsRealtime waitRealSec = new(0.03f);

    public static WaitForSecondsRealtime EffectAndGetWaiting(TextMeshProUGUI tmp, string str, float delay = 0, bool recursive = false, string currentStr = "")
    {
        CoroutineHelper.StartCoroutine(TypeEffecting(tmp, str, recursive, currentStr));

        return new(waitRealSec.waitTime * str.Length + delay);
    }
    public static (Coroutine, WaitForSecondsRealtime) GetEffectAndWaiting(TextMeshProUGUI tmp, string str, float delay = 0, bool recursive = false, string currentStr = "")
    {
        Coroutine coroutine = CoroutineHelper.StartCoroutine(TypeEffecting(tmp, str, recursive, currentStr));

        return (coroutine, new(waitRealSec.waitTime * str.Length + delay));
    }
    public static IEnumerator TypeEffecting(TextMeshProUGUI tmp, string str, bool recursive = false, string currentStr = "")
    {
        StringBuilder builder = new();
        string text;

        if(currentStr == "")
        {
            builder.Append(tmp.text);
        }
        else
        {
            builder.Append(currentStr);
        }

        if(recursive)
        {
            TextMeshProUGUI[] tmpArray = tmp.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

            for(int i = 0; i < str.Length; i++)
            {
                yield return waitRealSec;

                builder.Append(str[i]);

                text = builder.ToString();

                foreach (TextMeshProUGUI _tmp in tmpArray)
                {
                    _tmp.text = text;
                }
            }
        }
        else
        {
            for(int i = 0; i < str.Length; i++)
            {
                yield return waitRealSec;

                builder.Append(str[i]);

                tmp.text = builder.ToString();
            }
        }
    }
    public static IEnumerator EraseEffecting(TextMeshProUGUI tmp, float duration)
    {
        WaitForSecondsRealtime waitRealSec;
        StringBuilder builder = new();
        int count = 0;

        builder.Append(tmp.text);

        for(int i = 0; i < builder.Length; i++)
        {
            if(builder[i] != '\r' || builder[i] != '\n')
            {
                count++;
            }
        }

        waitRealSec = new(duration / count);

        while(builder.Length > 0)
        {
            yield return waitRealSec;

            IsNewLineStart(ref builder);

            builder.Remove(0, 1);

            tmp.text = builder.ToString();
        }
    }
    public static IEnumerator EraseEffecting(TextMeshProUGUI tmp, int targetCount = 0)
    {
        StringBuilder builder = new();

        builder.Append(tmp.text);

        while(builder.Length > targetCount)
        {
            yield return waitRealSec;

            IsNewLineStart(ref builder);

            builder.Remove(0, 1);

            tmp.text = builder.ToString();
        }
    }
    private static void IsNewLineStart(ref StringBuilder builder)
    {
        if(builder[0] == '\r' || builder[0] == '\n')
        {
            builder.Remove(0, 1);

            IsNewLineStart(ref builder);
        }
    }
}