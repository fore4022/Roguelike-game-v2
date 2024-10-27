using System.Collections;
using UnityEngine;
public class UIElementUtility
{
    public IEnumerator SetImageScale(RectTransform rectTransform, float targetScale, float duration = 1)
    {
        Vector3 scale;

        float totalTime = 0;
        float scaleValue;

        while(rectTransform.localScale.x != targetScale)
        {
            totalTime += Time.unscaledDeltaTime;

            if(totalTime > 1)
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