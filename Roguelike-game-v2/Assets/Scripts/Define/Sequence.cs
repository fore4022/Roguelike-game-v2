using System.Collections.Generic;
using UnityEngine;
public class Sequence
{
    private Queue<List<Coroutine>> tweenQueue = new();

    public int Count()
    {
        return tweenQueue.Count;
    }
    public List<Coroutine>[] Values()
    {
        return tweenQueue.ToArray();
    }
    public List<Coroutine> Peek()
    {
        if(tweenQueue.Count == 0)
        {
            tweenQueue.Enqueue(new());
        }

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
                TweenSystemManage.Clear(transform);
            }
        }
    }
}