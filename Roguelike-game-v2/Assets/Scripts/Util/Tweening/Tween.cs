using System.Collections;
using UnityEngine;
public static class Tween
{
    public delegate float EaseDelegate(float f);

    private static Easing easing = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.InLinear)
    {
        Util.GetMonoBehaviour().StartCoroutine(ScaleOverTime(easing.Get(ease), transform, targetScale, duration));

        return transform;
    }
    private static IEnumerator ScaleOverTime(EaseDelegate ease, Transform transform, float targetScale, float duration)
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