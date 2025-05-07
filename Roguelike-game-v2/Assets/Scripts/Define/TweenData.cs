using UnityEngine;
public class TweenData
{
    public Coroutine coroutine;
    public TweenType type;
    public Transform trans;
    public EaseDelegate easeDel;
    public NumericValue numeric;
    public float duration;
    public float delay;

    public TweenData() { }
    public void Set(TweenType type, Transform trans, EaseDelegate easeDel, NumericValue numeric, float duration)
    {
        this.type = type;
        this.trans = trans;
        this.easeDel = easeDel;
        this.numeric = numeric;
        this.duration = duration;
    }
    public void Set(Coroutine coroutine, float delay)
    {
        this.coroutine = coroutine;
        this.delay = delay;
    }
    public void Set(Coroutine coroutine)
    {
        this.coroutine = coroutine;
    }
}