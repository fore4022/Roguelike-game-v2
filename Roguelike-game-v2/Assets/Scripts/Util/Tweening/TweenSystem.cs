using UnityEngine;
/// <summary>
/// <para>
/// Component를 통해서 접근 가능한 트윈의 확장
/// </para>
/// 트윈 생성 및 관리 기능을 제공
/// </summary>
public static class TweenSystem
{
    // Scale
    public static Component SetScale(this Component comp, float targetScale, float duration, EaseType ease = EaseType.Linear, TweenOperation op = TweenOperation.Join)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Scale, new(targetScale), duration, ease);
    }
    public static Component SetScale(this Component comp, float targetScale, float duration, TweenOperation op, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Scale, new(targetScale), duration, ease);
    }
    public static Component SetScale(this Component comp, float targetScale, float duration, float delay, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Insert, TweenType.Scale, new(targetScale), duration, ease, delay);
    }
    public static Component SetScale(this Component comp, float targetScale)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Join, TweenType.Scale, new(targetScale), 0, EaseType.Linear, 0);
    }

    // Position
    public static Component SetPosition(this Component comp, Vector3 targetPosition, float duration, EaseType ease = EaseType.Linear, TweenOperation op = TweenOperation.Join)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Position, new(targetPosition), duration, ease);
    }
    public static Component SetPosition(this Component comp, Vector3 targetPosition, float duration, TweenOperation op, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Position, new(targetPosition), duration, ease);
    }
    public static Component SetPosition(this Component comp, Vector3 targetPosition, float duration, float delay, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Insert, TweenType.Position, new(targetPosition), duration, ease, delay);
    }
    public static Component SetPosition(this Component comp, Vector3 targetPosition)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Join, TweenType.Position, new(targetPosition), 0, EaseType.Linear, 0);
    }

    // Rotation
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, EaseType ease = EaseType.Linear, TweenOperation op = TweenOperation.Join)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Rotation, new(targetRotation), duration, ease);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, TweenOperation op, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, op, TweenType.Rotation, new(targetRotation), duration, ease);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation, float duration, float delay, EaseType ease = EaseType.Linear)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Insert, TweenType.Rotation, new(targetRotation), duration, ease, delay);
    }
    public static Component SetRotation(this Component comp, Vector3 targetRotation)
    {
        return Tween_Manage.Execute(comp, TweenOperation.Join, TweenType.Rotation, new(targetRotation), 0, EaseType.Linear, 0);
    }

    // Manage
    public static Component Stop(this Component comp)
    {
        return Tween_Manage.SetStatus(comp, false);
    }
    public static Component Play(this Component comp)
    {
        return Tween_Manage.SetStatus(comp, true);
    }
    public static Component PlayNext(this Component comp)
    {
        return Tween_Manage.PlayNext(comp);
    }
    public static Component Kill(this Component comp)
    {
        return Tween_Manage.Kill(comp);
    }
    public static Component SkipToEnd(this Component comp)
    {
        return Tween_Manage.SkipToEnd(comp);
    }
}