using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 하나의 객체가 가지는 Tween 정보를 저장 및 관리하는 타입
/// </summary>
public class Sequence
{
    private Queue<List<TweenData>> tweenQueue = new();

    public List<TweenData>[] Values()
    {
        return tweenQueue.ToArray();
    }
    public List<TweenData> PeekLast()
    {
        return tweenQueue.ToArray()[tweenQueue.Count - 1];
    }
    public List<TweenData> Peek()
    {
        if(tweenQueue.Count == 0)
        {
            tweenQueue.Enqueue(new());
        }

        return tweenQueue.Peek();
    }
    public int Count()
    {
        return tweenQueue.Count;
    }
    public void Enqueue(List<TweenData> list)
    {
        tweenQueue.Enqueue(list);
    }
    public void Dequeue(Transform transform, TweenData data)
    {
        tweenQueue.Peek().Remove(data);

        if(tweenQueue.Peek().Count == 0)
        {
            tweenQueue.Dequeue();

            if(tweenQueue.Count == 0)
            {
                Tween_Manage.Clear(transform);
            }
            else
            {
                foreach(TweenData _data in tweenQueue.Peek())
                {
                    _data.Set(CoroutineHelper.Start(Tweening.OverTime(_data.type, _data, _data.trans, _data.easeDel, _data.targetValue, _data.duration), CoroutineType.Tween));
                }
            }
        }
    }
}