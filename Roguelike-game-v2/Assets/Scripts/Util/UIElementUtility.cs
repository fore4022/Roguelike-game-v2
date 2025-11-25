using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public static class UIElementUtility
{
    public static Coroutine SetTextAlpha(TextMeshProUGUI tmp, float targetAlphaValue, float duration = 0, bool recursive = true)
    {
        List<TextMeshProUGUI> tmpList = new();

        Coroutine coroutine = null;
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

        if(duration == 0)
        {
            foreach(TextMeshProUGUI _tmp in tmpList)
            {
                color = _tmp.color;
                color.a = targetAlphaValue;
                _tmp.color = color;
            }
        }
        else
        {
            coroutine = CoroutineHelper.Start(SetTextAlpha(tmpList, color, targetAlphaValue, duration), CoroutineType.UserInterface);
        }

        return coroutine;
    }
    public static Coroutine SetImageAlpha(Image img, float targetAlphaValue, float duration = 0, bool recursive = true)
    {
        List<Image> imgList = new();

        Coroutine coroutine = null;
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

        if(duration == 0)
        {
            foreach(Image _img in imgList)
            {
                color = _img.color;
                color.a = targetAlphaValue;
                _img.color = color;
            }
        }
        else
        {
            coroutine = CoroutineHelper.Start(SetImageAlpha(imgList, color, targetAlphaValue, duration), CoroutineType.UserInterface);
        }

        return coroutine;
    }
    public static IEnumerator BlinkText(TextMeshProUGUI tmp, float duration, bool recursive = true, int minAlpha = 145, int maxAlpha = 255)
    {
        WaitForSeconds delay = new(duration);

        while(true)
        {
            SetTextAlpha(tmp, minAlpha, duration, recursive);

            yield return delay;

            SetTextAlpha(tmp, maxAlpha, duration, recursive);

            yield return delay;
        }
    }
    private static IEnumerator SetTextAlpha(List<TextMeshProUGUI> tmpList, Color color, float targetAlphaValue, float duration)
    {
        Color childrenColor;
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

            foreach(TextMeshProUGUI tmp in tmpList)
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