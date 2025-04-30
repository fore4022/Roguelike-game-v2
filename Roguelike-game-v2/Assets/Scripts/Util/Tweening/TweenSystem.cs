using UnityEngine;
public static class TweenSystem
{
    private static TweenSystemManage manage = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    { 
        manage.ExecuteTween(TweenType.Scale, transform, new(targetScale), duration, ease);

        return transform;
    }
    public static RectTransform SetScale(this RectTransform rectTransform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        manage.ExecuteTween(TweenType.Scale, rectTransform, new(targetScale), duration, ease);

        return rectTransform;
    }
    public static Transform SetPosition(this Transform transform, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        manage.ExecuteTween(TweenType.Position, transform, new(targetPosition), duration, ease);

        return transform;
    }
    public static Transform SetRotation(this Transform transform, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        manage.ExecuteTween(TweenType.Rotation, transform, new(targetRotation), duration, ease);

        return transform;
    }
}