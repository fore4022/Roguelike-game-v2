using System.Collections;
using UnityEngine;
public static class ColorUtil
{
    public static IEnumerator ChangeColor(SpriteRenderer render, Color targetColor, Color defaultColor, float duration)
    {
        float totalTime = 0;

        while(totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > duration)
            {
                totalTime = duration;
            }

            render.color = Color.Lerp(defaultColor, targetColor, totalTime / duration);

            yield return null;
        }
    }
    public static IEnumerator ChangeAlpha(SpriteRenderer render, float targetValue, float defaultValue, float duration)
    {
        Color color = render.color;
        float totalTime = 0;

        if(targetValue > 1)
        {
            targetValue /= 255;
        }

        if(defaultValue > 1)
        {
            defaultValue /= 255;
        }

        while(totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > duration)
            {
                totalTime = duration;
            }

            color.a = Mathf.Lerp(defaultValue, targetValue, totalTime / duration);
            render.color = color;

            yield return null;
        }
    }
}