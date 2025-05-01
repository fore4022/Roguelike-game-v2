using System;
using System.Collections.Generic;
using UnityEngine;
public class TweenSystemManage
{
    private static Tween tween = new();

    private static Dictionary<Component, Queue<List<Coroutine>>> tweenSchedule = new();
    private static Dictionary<Component, bool> tweenStatus = new();

    private readonly Type transform = typeof(Transform);
    private readonly Type rectTransform = typeof(RectTransform);

    public void ExecuteTween(TweenType type, Component comp, FlexibleValue flexible, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = null;
        EaseDelegate easeDelegate = Easing.Get(ease);
        Type t = comp.GetType();

        switch(type)
        {
            case TweenType.Scale:
                ScaleTweening(ref coroutine, t, comp, flexible, duration, easeDelegate);
                break;
            case TweenType.Rotation:
                PositionTweening(ref coroutine, t, comp, flexible, duration, easeDelegate);
                break;
            case TweenType.Position:
                RotationTweening(ref coroutine, t, comp, flexible, duration, easeDelegate);
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
    private void ScaleTweening(ref Coroutine coroutine, Type type, Component comp, FlexibleValue flexible, float duration, EaseDelegate ease)
    {
        if(type == transform)
        {
            coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(ease, comp as Transform, flexible.Float, duration));
        }
        else if(type == rectTransform)
        {
            coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.ScaleOverTime(ease, comp as RectTransform, flexible.Float, duration));
        }
    }
    private void PositionTweening(ref Coroutine coroutine, Type type, Component comp, FlexibleValue flexible, float duration, EaseDelegate ease)
    {
        if(type == transform)
        {
            coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.PositionOverTime(ease, comp as Transform, flexible.Vector, duration));
        }
    }
    private void RotationTweening(ref Coroutine coroutine, Type type, Component comp, FlexibleValue flexible, float duration, EaseDelegate ease)
    {
        if(type == transform)
        {
            coroutine = Util.GetMonoBehaviour().StartCoroutine(tween.RotationOverTime(ease, comp as Transform, flexible.Vector, duration));
        }
    }
}