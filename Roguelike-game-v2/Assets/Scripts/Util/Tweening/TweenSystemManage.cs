using System;
using System.Collections.Generic;
using UnityEngine;
public static class TweenSystemManage
{
    private static Tweening _tweening = new();

    private static Dictionary<Component, Sequence> _schedule = new();
    private static Dictionary<Component, TweenStatus> _status = new();

    private static readonly Type _transform = typeof(Transform);

    public static TweenStatus GetStatus(Component comp)
    {
        if(_status.TryGetValue(comp, out TweenStatus value))
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
        _status[comp].flag = status;
    }
    public static void Execute(TweenType type, Component comp, FlexibleValue flexible, float duration, Ease ease = Ease.Linear)
    {
        Coroutine coroutine = null;
        EaseDelegate easeDel = Easing.Get(ease);
        Transform trans;
        Tween tween = null;

        if(comp.Equals(_transform))
        {
            trans = comp as Transform;
        }
        else
        {
            trans = comp.GetComponent<Transform>();
        }

        if(trans == null)
        {
            return;
        }
        else
        {
            tween = new();
        }

        switch(type)
        {
            case TweenType.Scale:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tweening.OverTime(TweenType.Scale, tween, trans, easeDel, flexible, duration));
                break;
            case TweenType.Position:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tweening.OverTime(TweenType.Position, tween, trans, easeDel, flexible, duration));
                break;
            case TweenType.Rotation:
                coroutine = Util.GetMonoBehaviour().StartCoroutine(_tweening.OverTime(TweenType.Rotation, tween, trans, easeDel, flexible, duration));
                break;
        }

        tween.coroutine = coroutine;

        if(_schedule.TryGetValue(trans, out Sequence schedule))
        {
            schedule.Peek().Add(coroutine);
        }
        else
        {
            List<Coroutine> sched = new() { coroutine };

            schedule = new();

            schedule.Enqueue(sched);
            _schedule.Add(trans, schedule);
            _status.Add(trans, new(true));
        }
    }
    public static void Release(Transform transform, Tween tween)
    {
        _schedule[transform].Dequeue(transform, tween.coroutine);
    }
    public static void Clear(Transform transform)
    {
        _schedule.Remove(transform);
        _status.Remove(transform);
    }
    public static void Kill(Component comp)
    {
        Transform trans;

        if(comp.Equals(_transform))
        {
            trans = comp as Transform;
        }
        else
        {
            trans = comp.GetComponent<Transform>();
        }

        if(trans == null)
        {
            return;
        }

        if(_schedule.TryGetValue(trans, out Sequence sequence))
        {
            foreach(Coroutine coroutine in sequence.Values()[0])
            {
                Util.GetMonoBehaviour().StopCoroutine(coroutine);
            }

            Clear(trans);
        }
    }
}