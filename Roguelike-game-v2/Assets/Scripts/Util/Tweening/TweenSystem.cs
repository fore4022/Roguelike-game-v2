     using UnityEngine;
public static class TweenSystem
{
    public static Component SetScale(this Component comp, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(comp, TweenType.Scale, new(targetScale), duration, ease);

        return comp;
    }
    public static Component SetPosition(this Component comp, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(comp, TweenType.Position, new(targetPosition), duration, ease);

        return comp;
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Execute(comp, TweenType.Rotation, new(targetRotation), duration, ease);

        return comp;
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
    public static void TweenAppend(this Component comp, TweenType type, NumericValue numeric, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Append(comp, type, numeric, duration, ease);
    }
    public static void TweenInsert(this Component comp, TweenType type, NumericValue numeric, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Insert(comp, type, numeric, duration, ease);
    }
    public static void TweenJoin(this Component comp, TweenType type, NumericValue numeric, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Join(comp, type, numeric, duration, ease);
    }
    public static void TweenPrepend(this Component comp, TweenType type, NumericValue numeric, float duration, Ease ease = Ease.Linear)
    {
        TweenSystemManage.Prepend(comp, type, numeric, duration, ease);
    }
}