using System.Collections.Generic;
using UnityEngine;
public class Sequence
{
    private Queue<List<TweenData>> tweenQueue = new();

    public int Count()
    {
        return tweenQueue.Count;
    }
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
                TweenSystemManage.Clear(transform);
            }
            else
            {
                foreach(TweenData _data in tweenQueue.Peek())
                {
                    _data.Set(Util.GetMonoBehaviour().StartCoroutine(Tweening.OverTime(_data.type, _data, _data.trans, _data.easeDel, _data.targetValue, _data.duration)));
                }
            }
        }
    }
}