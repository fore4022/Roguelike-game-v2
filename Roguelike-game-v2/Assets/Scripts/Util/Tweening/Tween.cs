using System.Collections;
using UnityEngine;
public delegate float EaseDelegate(float f);
public class Tween
{
    public IEnumerator ScaleOverTime(EaseDelegate ease, Transform transform, float targetScale, float duration)
    {
        Vector2 scale = new();
        float initialScale = transform.localScale.x;
        float currentTime = 0;
        float scaleValue;

        while (currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                currentTime = duration;
            }

            scaleValue = Mathf.Lerp(initialScale, targetScale, ease(currentTime / duration));

            scale.x = scaleValue;
            scale.y = scaleValue;
            transform.localScale = scale;

            yield return null;
        }
    }
}