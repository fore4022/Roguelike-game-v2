using UnityEngine;
public static class TweenSystem
{
    // Scale
    public static Component SetScale(this Component comp, float targetScale, float duration, Ease ease = Ease.Linear, TweenOperation op = TweenOperation.Join)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Scale, new(targetScale), duration, ease);
    }
    public static Component SetScale(this Component comp, float targetScale, float duration, TweenOperation op, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Scale, new(targetScale), duration, ease);
    }
    public static Component SetScale(this Component comp, float targetScale, float duration, float delay, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Insert, TweenType.Scale, new(targetScale), duration, ease, delay);
    }
    public static Component SetScale(this Component comp, float targetScale)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Join, TweenType.Scale, new(targetScale), 0, Ease.Linear, 0);
    }

    // Position
    public static Component SetPosition(this Component comp, Vector2 targetPosition, float duration, Ease ease = Ease.Linear, TweenOperation op = TweenOperation.Join)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Position, new(targetPosition), duration, ease);
    }
    public static Component SetPosition(this Component comp, Vector2 targetPosition, float duration, TweenOperation op, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Position, new(targetPosition), duration, ease);
    }
    public static Component SetPosition(this Component comp, Vector2 targetPosition, float duration, float delay, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Insert, TweenType.Position, new(targetPosition), duration, ease, delay);
    }
    public static Component SetPosition(this Component comp, Vector2 targetPosition)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Join, TweenType.Position, new(targetPosition), 0, Ease.Linear, 0);
    }

    // Rotation
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, Ease ease = Ease.Linear, TweenOperation op = TweenOperation.Join)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Rotation, new(targetRotation), duration, ease);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, TweenOperation op, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, op, TweenType.Rotation, new(targetRotation), duration, ease);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, float delay, Ease ease = Ease.Linear)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Insert, TweenType.Rotation, new(targetRotation), duration, ease, delay);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation)
    {
        return TweenSystemManage.Execute(comp, TweenOperation.Join, TweenType.Rotation, new(targetRotation), 0, Ease.Linear, 0);
    }

    // Manage
    public static Component Stop(this Component comp)
    {
        return TweenSystemManage.SetStatus(comp, false);
    }
    public static Component Play(this Component comp)
    {
        return TweenSystemManage.SetStatus(comp, true);
    }
    public static Component PlayNext(this Component comp)
    {
        return TweenSystemManage.PlayNext(comp);
    }
    public static Component Kill(this Component comp)
    {
        return TweenSystemManage.Kill(comp);
    }
    public static Component SkipToEnd(this Component comp)
    {
        return TweenSystemManage.SkipToEnd(comp);
    }
}