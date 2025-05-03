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
        }
    }
}