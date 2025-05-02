using UnityEngine;
public static class TweenSystem
{
    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(TweenType.Scale, transform, new(targetScale), duration, ease);

        return transform;
    }
    public static Transform SetPosition(this Transform transform, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(TweenType.Position, transform, new(targetPosition), duration, ease);

        return transform;
    }
    public static Transform SetRotation(this Transform rectTransform, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(TweenType.Rotation, rectTransform, new(targetRotation), duration, ease);

        return rectTransform;
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
    public static void KillTween(this Transform transform)
    {
        //TweenSystemManage
    }
}