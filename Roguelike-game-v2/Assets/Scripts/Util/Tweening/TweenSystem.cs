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

    // Manage
    public static Component StopTween(this Component comp)
    {
        TweenSystemManage.SetStatus(comp, false);

        return comp;
    }
    public static Component PlayTween(this Component comp)
    {
        TweenSystemManage.SetStatus(comp, true);

        return comp;
    }
    public static void KillTween(this Component comp)
    {
        TweenSystemManage.Kill(comp);
    }
}