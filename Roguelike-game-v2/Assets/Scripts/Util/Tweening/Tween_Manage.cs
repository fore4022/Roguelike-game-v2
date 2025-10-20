using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// <para>
/// Ʈ�� �ִϸ��̼� ���� �� ����
/// </para>
/// Transform�� ������ Ʈ���� ���, ����, ���� ��� ����
/// </summary>
public static class Tween_Manage
{
    private static Dictionary<Component, Sequence> _schedule = new();
    private static Dictionary<Component, TweenStatus> _status = new();

    private static readonly Type _transform = typeof(Transform);

    // ������Ʈ�� Transform���� Ʈ���� ���� _schedule�� ���, TweenOperation�� ���� Tween ���� ������ ����
    public static Component Execute(Component comp, TweenOperation op, TweenType type, NumericValue numeric, float duration, EaseType ease, float delay = 0)
    {
        if(GetTransform(comp, out Transform trans))
        {
            TweenData data = new();

            bool isContain = _schedule.TryGetValue(trans, out Sequence schedule);

            switch(op)
            {
                case TweenOperation.Append:
                    data.Set(type, trans, EaseProvider.Get(ease), numeric, duration);

                    if(!isContain)
                    {
                        data.Set(Coroutine_Helper.StartCoroutine(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay)));

                        op = TweenOperation.Join;
                    }
                    break;
                case TweenOperation.Insert:
                    data.Set(Coroutine_Helper.StartCoroutine(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay)), type, trans, EaseProvider.Get(ease), numeric, duration);
                    break;
                case TweenOperation.Join:
                    if(isContain)
                    {
                        if(schedule.Count() > 1)
                        {
                            data.Set(type, trans, EaseProvider.Get(ease), numeric, duration);
                            break;
                        }
                    }

                    data.Set(Coroutine_Helper.StartCoroutine(Tweening.OverTime(type, data, trans, EaseProvider.Get(ease), numeric, duration, delay)), type, trans, EaseProvider.Get(ease), numeric, duration);
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
    // Ʈ�� �ִϸ��̼� ���� Ȱ��ȭ ���� ����
    public static Component SetStatus(Component comp, bool status)
    {
        _status[comp].flag = status;

        return comp;
    }
    // Ʈ�� �ߴ� �� ����
    public static Component Kill(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                foreach(TweenData data in sequence.Values()[0])
                {
                    if(data.coroutine != null)
                    {
                        Coroutine_Helper.StopCoroutine(data.coroutine);
                    }
                }

                Clear(trans);
            }
        }

        return comp;
    }
    // ���� ������ Ʈ���� �����ϸ�, ���� �ܰ��� Ʈ���� ����
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
    // ���� ���� ���� Ʈ���� �Ϸ� �������� ��ȯ
    public static Component SkipToEnd(Component comp)
    {
        if(GetTransform(comp, out Transform trans))
        {
            if(_schedule.TryGetValue(trans, out Sequence sequence))
            {
                List<TweenData> dataList = sequence.Peek();

                TweenData data = dataList[0];

                Coroutine_Helper.StopCoroutine(data.coroutine);
                Tweening.ToEnd(data);
            }
        }

        return comp;
    }
    // ��� Ʈ���� �Ϸ� �������� ��ȯ
    public static void AllSkipToEnd()
    {
        List<Component> keys = _schedule.Keys.ToList();

        for(int i = 0; i < keys.Count; i++)
        {
            SkipToEnd(keys[i]);
        }
    }
    // ��ϵ� Ʈ�� ���� Ȯ��
    public static bool IsTweenActive()
    {
        if(_schedule.Count != 0)
        {
            return true;
        }

        return false;
    }
    // ��� Ʈ�� ����, _schecule�� _status �ʱ�ȭ
    public static void Reset()
    {
        foreach(Sequence schedule in _schedule.Values)
        {
            foreach(List<TweenData> datas in schedule.Values())
            {
                foreach(TweenData data in datas)
                {
                    Coroutine_Helper.StopCoroutine(data.coroutine);
                }
            }
        }

        _schedule = new();
        _status = new();
    }
    // Ʈ�� ������ ���ؼ� ��ϵ� Ʈ�� ����
    public static void Release(Transform transform, TweenData data)
    {
        _schedule[transform].Dequeue(transform, data);
    }
    // Ʈ�� ����
    public static void Clear(Transform transform)
    {
        _schedule.Remove(transform);
        _status.Remove(transform);
    }
    // Ʈ�� Ȱ��ȭ ���� ��ȯ
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
    // Ʈ�� ��� �������� �Է¹��� ������Ʈ���� Transform ��ȯ
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