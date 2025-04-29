using System.Collections;
using UnityEngine;
public static class Tween
{
    public delegate float EaseDelegate(float f);

    private static Easing easing = new();

    public static EaseDelegate GetEasing(Ease ease)
    {
        EaseDelegate del = null;

        switch(ease)
        {
            case Ease.Linear:
                del += easing.Linear;
                break;
            case Ease.InQuad:
                del += easing.InQuad;
                break;
            case Ease.OutQuad:
                del += easing.OutQuad;
                break;
            case Ease.InOutQuad:
                del += easing.InOutQuad;
                break;
            case Ease.InCubic:
                del += easing.InCubic;
                break;
            case Ease.OutCubic:
                del += easing.OutCubic;
                break;
            case Ease.InOutCubic:
                del += easing.InOutCubic;
                break;
            case Ease.InQuart:
                del += easing.InQuart;
                break;
            case Ease.OutQuart:
                del += easing.OutQuart;
                break;
            case Ease.InOutQuart:
                del += easing.InOutQuart;
                break;
            case Ease.InQuint:
                del += easing.InQuint;
                break;
            case Ease.OutQuint:
                del += easing.OutQuint;
                break;
            case Ease.InOutQuint:
                del += easing.InOutQuint;
                break;
            case Ease.InSine:
                del += easing.InSine;
                break;
            case Ease.OutSine:
                del += easing.OutSine;
                break;
            case Ease.InOutSine:
                del += easing.InOutSine;
                break;
            case Ease.InExpo:
                del += easing.InExpo;
                break;
            case Ease.OutExpo:
                del += easing.OutExpo;
                break;
            case Ease.InOutExpo:
                del += easing.InOutExpo;
                break;
            case Ease.InCirc:
                del += easing.InCirc;
                break;
            case Ease.OutCirc:
                del += easing.OutCirc;
                break;
            case Ease.InOutCirc:
                del += easing.InOutCirc;
                break;
            case Ease.InBounce:
                del += easing.InBounce;
                break;
            case Ease.OutBounce:
                del += easing.OutBounce;
                break;
            case Ease.InOutBounce:
                del += easing.InOutBounce;
                break;
            case Ease.InBack:
                del += easing.InBack;
                break;
            case Ease.OutBack:
                del += easing.OutBack;
                break;
            case Ease.InOutBack:
                del += easing.InOutBack;
                break;
            case Ease.InElastic:
                del += easing.InElastic;
                break;
            case Ease.OutElastic:
                del += easing.OutElastic;
                break;
            case Ease.InOutElastic:
                del += easing.InOutElastic;
                break;
        }

        return del;
    }
    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        Util.GetMonoBehaviour().StartCoroutine(ScaleOverTime(GetEasing(ease), transform, targetScale, duration));

        return transform;
    }
    private static IEnumerator ScaleOverTime(EaseDelegate ease, Transform transform, float targetScale, float duration)
    {
        Vector2 scale = new();
        float currentTime = 0;
        float scaleValue = 0;

        while (currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if (currentTime > duration)
            {
                currentTime = duration;
            }

            //scaleValue = targetScale * ;

            yield return null;
        }
    }
}