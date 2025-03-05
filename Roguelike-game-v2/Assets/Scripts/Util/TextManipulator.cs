using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
public static class TextManipulator
{
    private static WaitForSecondsRealtime delay = new(0.02f);

    public static IEnumerator TypeEffecting(TextMeshProUGUI tmp, string str)
    {
        StringBuilder builder = new();

        for(int i = 0; i < str.Length; i++)
        {
            yield return delay;

            builder.Append(str[i]);

            tmp.text = builder.ToString();
        }
    }
}