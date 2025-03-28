using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public static class UIElementUtility
{
    private static float pressedColor = 1.25f;
    private static float normalizedColor = 0.8f;

    public static void SetButtonColor(Transform transform, bool isPressed)
    {
        List<Image> imageList = transform.GetComponentsInChildren<Image>().ToList();

        float value = isPressed ? pressedColor : normalizedColor;

        foreach(Image image in imageList)
        {
            Color color = image.color;

            color.r /= value;
            color.g /= value;
            color.b /= value;
            image.color = color;
        }
    }
    public static void SetImageScale(RectTransform rectTransform, float targetScale)
    {
        rectTransform.localScale = Calculate.GetVector(targetScale);
    }
    public static Coroutine SetTextAlpha(TextMeshProUGUI tmp, float targetAlphaValue, float duration, bool recursive = true)
    {
        List<TextMeshProUGUI> tmpList = new();

        Color color = tmp.color;
        targetAlphaValue /= 255;

        if(recursive)
        {
            tmpList = tmp.gameObject.GetComponentsInChildren<TextMeshProUGUI>().ToList();
        }
        else
        {
            tmpList.Add(tmp);
        }

        return Util.GetMonoBehaviour().StartCoroutine(SetTextAlpha(tmpList, color, targetAlphaValue, duration));
    }
    public static void SetImageAlpha(Image img, float targetAlphaValue, float duration = 0, bool recursive = true)
    {
        List<Image> imgList = new();

        Color color = img.color;
        targetAlphaValue /= 255;

        if(recursive)
        {
            imgList = img.gameObject.GetComponentsInChildren<Image>().ToList();
        }
        else
        {
            imgList.Add(img);
        }

        Util.GetMonoBehaviour().StartCoroutine(SetImageAlpha(imgList, color, targetAlphaValue, duration));
    }
    public static IEnumerator BlinkText(TextMeshProUGUI tmp, int minAlpha, int maxAlpha, float duration, bool recursive = true)
    {
        WaitForSeconds delay = new(duration);

        while (true)
        {
            SetTextAlpha(tmp, minAlpha, duration, recursive);

            yield return delay;

            SetTextAlpha(tmp, maxAlpha, duration, recursive);

            yield return delay;
        }
    }
    public static IEnumerator SetImageScale(RectTransform rectTransform, float targetScale, float duration)
    {
        Vector3 scale = new();
        float totalTime = 0;
        float scaleValue;

        while (totalTime != 1)
        {
            totalTime += Time.unscaledDeltaTime;

            if (totalTime > duration)
            {
                totalTime = 1;
            }

            scaleValue = Mathf.Lerp(rectTransform.localScale.x, targetScale, totalTime);
            scale.x = scaleValue;
            scale.y = scaleValue;
            rectTransform.localScale = scale;

            yield return null;
        }
    }
    private static IEnumerator SetTextAlpha(List<TextMeshProUGUI> tmpList, Color color, float targetAlphaValue, float duration)
    {
        Color childrenColor;
        float totalTime = 0;
        float alphaValue = color.a;

        while (totalTime != duration)
        {
            totalTime += Time.unscaledDeltaTime;

            if (totalTime > duration)
            {
                totalTime = duration;
            }

            color.a = Mathf.Lerp(alphaValue, targetAlphaValue, totalTime / duration);

            foreach (TextMeshProUGUI tmp in tmpList)
            {
                childrenColor = tmp.color;
                childrenColor.a = color.a;
                tmp.color = childrenColor;
            }

            yield return null;
        }
    }
    private static IEnumerator SetImageAlpha(List<Image> imgList, Color color, float targetAlphaValue, float duration)
    {
        Color childrenColor;

        if(duration == 0)
        {
            color.a = targetAlphaValue;

            foreach(Image img in imgList)
            {
                childrenColor = img.color;
                childrenColor.a = targetAlphaValue;
                img.color = childrenColor;
            }

            yield return null;
        }
        else
        {
            float totalTime = 0;
            float alphaValue = color.a;

            while(totalTime != duration)
            {
                totalTime += Time.unscaledDeltaTime;

                if(totalTime > duration)
                {
                    totalTime = duration;
                }

                color.a = Mathf.Lerp(alphaValue, targetAlphaValue, totalTime / duration);

                foreach(Image img in imgList)
                {
                    childrenColor = img.color;
                    childrenColor.a = color.a;
                    img.color = childrenColor;
                }

                yield return null;
            }
        }
    }
}