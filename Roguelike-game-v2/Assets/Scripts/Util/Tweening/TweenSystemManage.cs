using System;
using System.Collections.Generic;
using UnityEngine;
public class Sequence
{
    private Queue<List<Coroutine>> tweenQueue = new();

    public List<Coroutine> Peek()
    {
        return tweenQueue.Peek();
    }
    public void Enqueue(List<Coroutine> list)
    {
        tweenQueue.Enqueue(list);
    }
    public void Dequeue(Transform transform, Coroutine coroutine)
    {
        tweenQueue.Peek().Remove(coroutine);

        if(tweenQueue.Peek().Count == 0)
        {
            tweenQueue.Dequeue();

            if(tweenQueue.Count == 0)
            {
                TweenSystem.KillTween(transform);
            }
            else
            {

            }
        }
    }
}
public class Status
{
    public bool flag;

    public Status(bool status)
    {
        flag = status;
    }
}
public class Tween
{
    public Coroutine coroutine;

    public Tween() { }
    public Tween(Coroutine coroutine)
    {
        this.coroutine = coroutine;
    }
}
public static class TweenSystemManage
{
    private static Tweening _tweening = new();

    private static Dictionary<Component, Sequence> _schedule = new();
    private static Dictionary<Component, Status> _status = new();

    private static readonly Type _transform = typeof(Transform);

    public static Status GetStatus(Component comp)
    {
        if(_status.TryGetValue(comp, out Status value))
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
        Transform trans = null;
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

        if(_schedule.TryGetValue(comp, out Sequence schedule))
        {
            schedule.Peek().Add(coroutine);
        }
        else
        {
            List<Coroutine> sched = new() { coroutine };

            schedule = new();

            schedule.Enqueue(sched);
            _schedule.Add(comp, schedule);
            _status.Add(comp, new(true));
        }
    }
    public static void Release(Transform transform, Tween tween)
    {
        _schedule[transform].Dequeue(transform, tween.coroutine);
    }
    public static void Kill(Component comp)
    {
        Debug.Log("g");

        //if(_schedule.TryGetValue(comp, out ))
        //{

        //}
    }
}