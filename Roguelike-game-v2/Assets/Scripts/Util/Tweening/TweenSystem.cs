using UnityEngine;
public static class TweenSystem
{
    // Scale
    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.ExecuteTween(TweenType.Scale, transform, new(targetScale), duration, ease);

        return transform;
    }
    public static RectTransform SetScale(this RectTransform rectTransform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.ExecuteTween(TweenType.Scale, rectTransform, new(targetScale), duration, ease);

        return rectTransform;
    }

    // Position
    public static Transform SetPosition(this Transform transform, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.ExecuteTween(TweenType.Position, transform, new(targetPosition), duration, ease);

        return transform;
    }

    // Rotation
    public static Transform SetRotation(this Transform transform, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.ExecuteTween(TweenType.Rotation, transform, new(targetRotation), duration, ease);

        return transform;
    }

    // Manage
    public static Transform StopTween(this Transform transform)
    {
        TweenSystemManage.SetStatus(transform, false);

        return transform;
    }
    public static Transform PlayTween(this Transform transform)
    {
        TweenSystemManage.SetStatus(transform, true);

        return transform;
    }
    //public static Transform Appeend(this Transform transform, )
    //{
    //    return transform;
    //}
}