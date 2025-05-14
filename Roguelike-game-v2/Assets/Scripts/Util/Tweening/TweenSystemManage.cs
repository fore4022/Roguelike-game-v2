using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public static class TweenSystemManage
{
    private static Dictionary<Component, Sequence> _schedule = new();
    private static Dictionary<Component, TweenStatus> _status = new();

    private static readonly Type _transform = typeof(Transform);

    public static Component Execute(Component comp, TweenOperation op, TweenType type, NumericValue numeric, float duration, Ease ease, float delay = 0)
    {
        if(GetTransform(comp, out Transform trans))
        {
            TweenData data = new();
            bool isContain = _schedule.TryGetValue(trans, out Sequence schedule);

            switch(op)
            {
                case TweenOperation.Append:
                    data.Set(type, trans, Easing.Get(ease), numeric, duration);

                    if(!isContain)
                    {
                        data.Set(Util.GetMonoBehaviour().StartCoroutine(Tweening.OverTime(type, data, trans, Easing.Get(ease), numeric, duration, delay)));

                        op = TweenOperation.Join;
                    }
                    break;
                case TweenOperation.Insert:
                    data.Set(Util.GetMonoBehaviour().StartCoroutine(Tweening.OverTime(type, data, trans, Easing.Get(ease), numeric, duration, delay)), type, trans, Easing.Get(ease), numeric, duration);
                    break;
                case TweenOperation.Join:
                    if(isContain)
                    {
                        if(schedule.Count() > 1)
                        {
                            data.Set(type, trans, Easing.Get(ease), numeric, duration);
                            break;
                        }
                    }

                    data.Set(Util.GetMonoBehaviour().StartCoroutine(Tweening.OverTime(type, data, trans, Easing.Get(ease), numeric, duration, delay)), type, trans, Easing.Get(ease), numeric, duration);
                    break;
            }

            if(isContain && op != TweenOperation.Append)
            {
                schedule.PeekLast().Add(data);
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

            return comp;
        }
        else
        {
            return null;
        }
    }
    public static Component SetStatus(Component comp, bool status)
    {
        _status[comp].flag = status;

        return comp;
    }
    public static Component Kill(Component comp)
    {
        if (GetTransform(comp, out Transform trans))
        {
            if (_schedule.TryGetValue(trans, out Sequence sequence))
            {
                foreach (TweenData data in sequence.Values()[0])
                {
                    if (data.coroutine != null)
                    {
                        Util.GetMonoBehaviour().StopCoroutine(data.coroutine);
                    }
                }

                Clear(trans);
            }
        }

        return comp;
    }
    public static Component PlayNext(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                List<TweenData> dataList = sequence.Peek();
                int count = dataList.Count;

                for (int i = 0; i < count; i++)
                {
                    Release(trans, dataList[0]);
                }
            }
        }

        return comp;
    }
    public static Component SkipToEnd(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                List<TweenData> dataList = sequence.Peek();
                int count = dataList.Count;

                for (int i = 0; i < count; i++)
                {
                    TweenData data = dataList[0];

                    Util.GetMonoBehaviour().StopCoroutine(data.coroutine);
                    Tweening.ToEnd(data);
                }
            }
        }

        return comp;
    }
    public static void AllSkipToEnd()
    {
        List<Component> keys = _schedule.Keys.ToList();

        for(int i = 0; i < keys.Count; i++)
        {
            SkipToEnd(keys[i]);
        }
    }
    public static void Reset()
    {
        foreach (Sequence schedule in _schedule.Values)
        {
            foreach (List<TweenData> datas in schedule.Values())
            {
                foreach (TweenData data in datas)
                {
                    Util.GetMonoBehaviour().StopCoroutine(data.coroutine);
                }
            }
        }

        _schedule = new();
        _status = new();
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
    public static TweenStatus GetStatus(Component comp)
    {
        if (_status.TryGetValue(comp, out TweenStatus value))
        {
            return value;
        }
        else
        {
            return null;
        }
    }
    private static bool GetTransform(Component comp, out Transform transform)
    {
        if(comp.Equals(_transform))
        {
            transform = comp as Transform;

            return true;
        }
        else
        {
            transform = comp.GetComponent<Transform>();

            return transform != null ? true : false;
        }
    }
}