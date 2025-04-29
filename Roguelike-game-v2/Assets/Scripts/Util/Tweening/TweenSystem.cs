using UnityEngine;
public static class TweenSystem
{
    public static Tween tween = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(Easing.Get(ease), transform, targetScale, duration));

        return transform;
    }
    public static Transform SetPosition(this Transform transform, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        Util.GetMonoBehaviour().StartCoroutine(tween.PositionOverTime(Easing.Get(ease), transform, targetPosition, duration));

        return transform;
    }
    public static Transform SetRotation(this Transform transform, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        Util.GetMonoBehaviour().StartCoroutine(tween.RotationOverTime(Easing.Get(ease), transform, targetRotation, duration));

        return transform;
    }
}