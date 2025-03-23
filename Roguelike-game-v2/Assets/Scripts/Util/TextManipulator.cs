using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
public static class TextManipulator
{
    private static WaitForSeconds delay = new(0.02f);

    public static IEnumerator TypeEffecting(TextMeshProUGUI tmp, string str, string currentStr = "")
    {
        StringBuilder builder = new();

        if(currentStr == "")
        {
            builder.Append(tmp.text);
        }
        else
        {
            builder.Append(currentStr);
        }

        for(int i = 0; i < str.Length; i++)
        {
            yield return delay;

            builder.Append(str[i]);

            tmp.text = builder.ToString();
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