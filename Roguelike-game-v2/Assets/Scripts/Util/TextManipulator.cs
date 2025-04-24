using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
public static class TextManipulator
{
    private static WaitForSecondsRealtime delay = new(0.075f);

    public static IEnumerator TypeEffecting(TextMeshProUGUI tmp, string str, bool recursive = false, bool isClear = false, string currentStr = "")
    {
        StringBuilder builder = new();

        if(isClear)
        {
            tmp.text = "";
        }

        if (currentStr == "")
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

            for (int i = 0; i < str.Length; i++)
            {
                yield return delay;

                builder.Append(str[i]);

                foreach(TextMeshProUGUI _tmp in tmpArray)
                {
                    _tmp.text = builder.ToString();
                }
            }
        }
        else
        {
            for(int i = 0; i < str.Length; i++)
            {
                yield return delay;

                builder.Append(str[i]);

                tmp.text = builder.ToString();
            }
        }
    }
    public static IEnumerator EraseEffecting(TextMeshProUGUI tmp,  int targetCount)
    {
        StringBuilder builder = new();

        builder.Append(tmp.text);

        while(builder.Length > targetCount)
        {
            yield return delay;

            builder.Remove(0, 1);

            tmp.text = builder.ToString();
        }
    }
}