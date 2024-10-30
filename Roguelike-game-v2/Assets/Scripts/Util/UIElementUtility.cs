using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIElementUtility : Selectable
{
    public void SetButtonState(Button button, int stateInt)
    {
        if (stateInt > 4) { return; }

        SelectionState state = (SelectionState)stateInt;

        //button = DoStateTransition(state, true);
    }
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
    public IEnumerator SetImageAlpha(Image image, float targetAlphaValue, float duration = 1, bool recursive = true)
    {
        List<Image> imageList = null;

        Color color = image.color;

        float totalTime = 0;

        targetAlphaValue /= 255;

        if (recursive)
        {
            imageList = Util.GetComponentsInChildren<Image>(image.transform);
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

            while (totalTime != 1)
            {
                totalTime += Time.deltaTime;

                if (totalTime > duration)
                {
                    totalTime = 1;
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