using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
public static class TextManipulator
{
    private static WaitForSeconds delay = new(0.01f);

    public static IEnumerator typeEffecting(TextMeshProUGUI tmp, string str)
    {
        StringBuilder builder = new();

        for(int i = 0; i < str.Length; i++)
        {
            builder.Append(str[i]);

            tmp.text = builder.ToString();

            yield return delay;
        }
    }
}