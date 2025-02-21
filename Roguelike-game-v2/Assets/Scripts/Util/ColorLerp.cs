using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public static class ColorLerp
{
    public static Color GetColor(float value)
    {
        return new Color(value, value, value);
    }
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
    public static IEnumerator ChangeColor(Image img, Color targetColor, Color defaultColor, float duration)
    {
        float totalTime = 0;

        while (totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if (totalTime > duration)
            {
                totalTime = duration;
            }

            img.color = Color.Lerp(defaultColor, targetColor, totalTime / duration);

            yield return null;
        }
    }
    public static IEnumerator ChangeAlpha(SpriteRenderer render, float targetValue, float defaultValue, float duration)
    {
        Color color = render.color;
        float totalTime = 0;

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
    public static IEnumerator ChangeAlpha(Image img, float targetValue, float defaultValue, float duration)
    {
        Color color = img.color;
        float totalTime = 0;

        while (totalTime != duration)
        {
            totalTime += Time.deltaTime;

            if (totalTime > duration)
            {
                totalTime = duration;
            }

            color.a = Mathf.Lerp(defaultValue, targetValue, totalTime / duration);
            img.color = color;

            yield return null;
        }
    }
}