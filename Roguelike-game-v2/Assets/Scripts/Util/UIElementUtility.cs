using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIElementUtility
{
    private const float pressedColor = 1.25f;
    private const float normalizedColor = 0.8f;

    public void SetButtonColor(Transform transform, bool isPressed)
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
    public Coroutine SetTextAlpha(TextMeshProUGUI tmp, float targetAlphaValue, float duration = 0, bool recursive = true)
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
    public Coroutine SetImageAlpha(Image img, float targetAlphaValue, float duration = 0, bool recursive = true)
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

        return Util.GetMonoBehaviour().StartCoroutine(SetImageAlpha(imgList, color, targetAlphaValue, duration));
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
    public IEnumerator SetTextAlpha(List<TextMeshProUGUI> tmpList, Color color, float targetAlphaValue, float duration)
    {
        Color childrenColor;

        float totalTime = 0;

        if (duration == 0)
        {
            color.a = targetAlphaValue;

            foreach(TextMeshProUGUI tmp in tmpList)
            {
                childrenColor = tmp.color;

                childrenColor.a = targetAlphaValue;

                tmp.color = childrenColor;
            }
        }
        else
        {
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
    }
    public IEnumerator SetImageAlpha(List<Image> imgList, Color color, float targetAlphaValue, float duration)
    {
        Color childrenColor;
        float totalTime = 0;

        if (duration == 0)
        {
            color.a = targetAlphaValue;

            foreach(Image img in imgList)
            {
                childrenColor = img.color;

                childrenColor.a = targetAlphaValue;

                img.color = childrenColor;
            }
        }
        else
        {
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