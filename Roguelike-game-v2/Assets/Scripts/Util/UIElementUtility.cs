using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UIElementUtility
{
    private const float pressedColor = 1.25f;
    private const float normalizedColor = 0.8f;

    public void SetButtonColor(Transform transform, bool isPressed)
    {
        List<Image> imageList = transform.GetComponentsInChildren<Image>().ToList();

        float value = (isPressed ? pressedColor : normalizedColor);

        foreach(Image image in imageList)
        {
            Color color = image.color;

            color.r /= value;
            color.g /= value;
            color.b /= value;

            image.color = color;
        }
    }
    public IEnumerator SetImageScale(RectTransform rectTransform, float targetScale, float duration = 0)
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
    public IEnumerator SetImageAlpha(Image image, float targetAlphaValue, float duration = 0, bool recursive = true)
    {
        List<Image> imageList = new();

        Color color = image.color;

        float totalTime = 0;

        targetAlphaValue /= 255;

        if (recursive)
        {
            imageList = image.gameObject.GetComponentsInChildren<Image>().ToList();
        }

        if (duration == 0)
        {
            color.a = targetAlphaValue;

            image.color = color;

            foreach (Image img in imageList)
            {
                Color childrenColor = img.color;

                childrenColor.a = targetAlphaValue;

                img.color = childrenColor;
            }
        }
        else
        {
            float alphaValue = color.a;

            while (totalTime != duration)
            {
                totalTime += Time.fixedDeltaTime;

                if (totalTime > duration)
                {
                    totalTime = duration;
                }

                color.a = Mathf.Lerp(alphaValue, targetAlphaValue, totalTime / duration);

                foreach(Image img in imageList)
                {
                    Color childrenColor = img.color;

                    childrenColor.a = color.a;

                    img.color = childrenColor;
                }

                image.color = color;

                yield return null;
            }
        }
    }
}