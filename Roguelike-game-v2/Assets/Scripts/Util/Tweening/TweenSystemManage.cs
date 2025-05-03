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
    public static void Execute(TweenType type, Component comp, NumericValue numeric, float duration, Ease ease = Ease.Linear)
    {
        EaseDelegate easeDel = Easing.Get(ease);
        Transform trans;
        TweenData data = null;

        trans = GetTransform(comp);

        if(trans == null)
        {
            return;
        }
        else
        {
            data = new(Util.GetMonoBehaviour().StartCoroutine(_tweening.OverTime(type, data, trans, easeDel, numeric, duration)));
        }

        if(_schedule.TryGetValue(trans, out Sequence schedule))
        {
            schedule.Peek().Add(data);
        }
        else 
        {
            List<TweenData> sched = new() { data };

            schedule = new();

            schedule.Enqueue(sched);
            _schedule.Add(trans, schedule);
            _status.Add(trans, new(true));
        }
    }
    public static void Release(Transform transform, TweenData tween)
    {
        _schedule[transform].Dequeue(transform, tween);
    }
    public static void Clear(Transform transform)
    {
        _schedule.Remove(transform);
        _status.Remove(transform);
    }
    public static void Kill(Component comp)
    {
        Transform trans = GetTransform(comp);

        if(trans == null)
        {
            return;
        }

        if(_schedule.TryGetValue(trans, out Sequence sequence))
        {
            foreach(TweenData data in sequence.Values()[0])
            {
                Debug.Log(data);
                Debug.Log(data.coroutine);

                Util.GetMonoBehaviour().StopCoroutine(data.coroutine);
            }

            Clear(trans);
        }
    }
    private static Transform GetTransform(Component comp)
    {
        if (comp.Equals(_transform))
        {
            return comp as Transform;
        }
        else
        {
            return comp.GetComponent<Transform>();
        }
    }
}