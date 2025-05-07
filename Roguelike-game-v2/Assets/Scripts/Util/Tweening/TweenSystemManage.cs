using System;
using System.Collections.Generic;
using UnityEngine;
public static class TweenSystemManage
{
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
    public static void Execute(Component comp, TweenOperation op, TweenType type, NumericValue numeric, float duration, Ease ease, float delay = 0)
    {
        Transform trans = GetTransform(comp);
        TweenData data = new();

        if(trans == null)
        {
            return;
        }
        else
        {
            if(op == TweenOperation.Append)
            {
                data.Set(type, trans, Easing.Get(ease), numeric, duration);
            }
            else
            {
                data.Set(Util.GetMonoBehaviour().StartCoroutine(Tweening.OverTime(type, data, trans, Easing.Get(ease), numeric, duration, delay)));
            }
        }

        if(_schedule.TryGetValue(trans, out Sequence schedule) && op != TweenOperation.Append)
        {
            schedule.Peek().Add(data);
        }
        else 
        {
            List<TweenData> sched = new() { data };

            if(op != TweenOperation.Append)
            {
                schedule = new();

                _schedule.Add(trans, schedule);
                _status.Add(trans, new(true));
            }

            schedule.Enqueue(sched);
        }      
    }
    public static void Release(Transform transform, TweenData data)
    {
        _schedule[transform].Dequeue(transform, data);
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
                if(data.coroutine != null)
                {
                    Util.GetMonoBehaviour().StopCoroutine(data.coroutine);
                }
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