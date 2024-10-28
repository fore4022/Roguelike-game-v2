using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class UIElementUtility
{
    public IEnumerator SetImageScale(RectTransform rectTransform, float targetScale, float duration = 1)
    {
        Vector3 scale;

        float totalTime = 0;

        if(duration == 0)
        {
            scale = new Vector2(targetScale, targetScale);

            rectTransform.localScale = scale;
        }
        else
        {
            float scaleValue;

            while (totalTime != 1)
            {
                totalTime += Time.unscaledDeltaTime;

                if (totalTime > duration)
                {
                    totalTime = 1;
                }

                scaleValue = Mathf.Lerp(rectTransform.localScale.x, targetScale, totalTime);

                scale = new Vector2(scaleValue, scaleValue);

                rectTransform.localScale = scale;

                yield return null;
            }
        }
    }
    public IEnumerator SetImageColor(Image image, float targetAlphaValue, float duration = 1)
    {
        Color color = image.color;

        float totalTime = 0;

        targetAlphaValue /= 255;

        if(duration == 0)
        {
            color.a = targetAlphaValue;

            image.color = color;
        }
        else
        {
            float alphaValue = color.a;

            while (totalTime != 1)
            {
                totalTime += Time.deltaTime;

                if (totalTime > duration)
                {
                    totalTime = 1;
                }

                color.a = Mathf.Lerp(alphaValue, targetAlphaValue, totalTime / duration);

                image.color = color;

                yield return null;
            }
        }
    }
}