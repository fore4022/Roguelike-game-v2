using UnityEngine;
public static class TweenSystem
{
    public static Tween tween = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.InLinear)
    {
        Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(Easing.Get(ease), transform, targetScale, duration));

        return transform;
    }
}