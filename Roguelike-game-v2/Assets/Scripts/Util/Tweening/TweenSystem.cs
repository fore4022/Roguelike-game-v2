using System.Collections.Generic;
using UnityEngine;
public static class TweenSystem
{
    private static Tween tween = new();

    private static Dictionary<Component, Queue<List<Coroutine>>> tweenSchedule = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration, Ease ease = Ease.Linear)
    { 
        ExecuteTween(TweenType.Scale, transform, new(targetScale), duration, ease);

        return transform;
    }
    public static RectTransform SetScale(this RectTransform rectTransform, float targetScale, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(Easing.Get(ease), rectTransform, targetScale, duration));

        //

        return rectTransform;
    }
    public static Transform SetPosition(this Transform transform, Vector2 targetPosition, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.PositionOverTime(Easing.Get(ease), transform, targetPosition, duration));

        return transform;
    }
    public static Transform SetRotation(this Transform transform, Vector3 targetRotation, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.RotationOverTime(Easing.Get(ease), transform, targetRotation, duration));

        return transform;
    }
    private static void ExecuteTween(TweenType type, Component comp, FlexibleValue flexible, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = null;

        switch(type)
        {
            case TweenType.Scale:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(Easing.Get(ease), comp as Transform, flexible.Float, duration));
                break;
            case TweenType.Position:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(Easing.Get(ease), comp as RectTransform, flexible.Float, duration));
                break;
            case TweenType.Rotation:
                break;
        }

        if(tweenSchedule.TryGetValue(comp, out Queue<List<Coroutine>> schedule))
        {
            schedule.Peek().Add(coroutine);
        }
        else
        {
            List<Coroutine> sched = new() { coroutine };

            schedule = new();

            schedule.Enqueue(sched);
            tweenSchedule.Add(comp, schedule);
        }
    }
}