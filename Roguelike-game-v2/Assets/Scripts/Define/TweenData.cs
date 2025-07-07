using UnityEngine;
public class TweenData
{
    public TweenType type;
    public NumericValue targetValue;
    public EaseDelegate easeDel;
    public Transform trans;

    public Coroutine coroutine;
    public float duration;

    public TweenData() { }
    public void Set(Coroutine coroutine, TweenType type, Transform trans, EaseDelegate easeDel, NumericValue targetValue, float duration)
    {
        this.coroutine = coroutine;
        this.type = type;
        this.trans = trans;
        this.easeDel = easeDel;
        this.targetValue = targetValue;
        this.duration = duration;
    }
    public void Set(TweenType type, Transform trans, EaseDelegate easeDel, NumericValue targetValue, float duration)
    {
        this.type = type;
        this.trans = trans;
        this.easeDel = easeDel;
        this.targetValue = targetValue;
        this.duration = duration;
    }
    public void Set(Coroutine coroutine)
    {
        this.coroutine = coroutine;
    }
}