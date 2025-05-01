using System;
using System.Collections.Generic;
using UnityEngine;
public class Status
{
    public bool flag;

    public Status(bool status)
    {
        flag = status;
    }
}
public static class TweenSystemManage
{
    private static Tween _tween = new();

    private static Dictionary<Component, Queue<List<Coroutine>>> _tweenSchedule = new();
    private static Dictionary<Component, Status> _tweenStatus = new();

    private static readonly Type _transform = typeof(Transform);

    public static Status GetStatus(Component comp)
    {
        if(_tweenStatus.TryGetValue(comp, out Status value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    public static void SetStatus(Component comp, bool status)
    {
        _tweenStatus[comp].flag = status;
    }
    public static void ExecuteTween(TweenType type, Component comp, FlexibleValue flexible, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = null;
        EaseDelegate easeDel = Easing.Get(ease);
        Transform trans = null;

        if(comp.Equals(_transform))
        {
            trans = comp as Transform;
        }
        else
        {
            trans = comp.GetComponent<Transform>();
        }

        switch(type)
        {
            case TweenType.Scale:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tween.OverTime(TweenType.Scale, easeDel, trans, flexible, duration));
                break;
            case TweenType.Position:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tween.OverTime(TweenType.Position, easeDel, trans, flexible, duration));
                break;
            case TweenType.Rotation:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tween.OverTime(TweenType.Rotation, easeDel, trans, flexible, duration));
                break;
        }

        if(_tweenSchedule.TryGetValue(comp, out Queue<List<Coroutine>> schedule))
        {
            schedule.Peek().Add(coroutine);
        }
        else
        {
            List<Coroutine> sched = new() { coroutine };

            schedule = new();

            schedule.Enqueue(sched);
            _tweenSchedule.Add(comp, schedule);
            _tweenStatus.Add(comp, new(true));
        }
    }
}