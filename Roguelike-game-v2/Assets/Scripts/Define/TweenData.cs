using UnityEngine;
public class TweenData
{
    public Coroutine coroutine;
    public TweenType type;
    public Transform trans;
    public EaseDelegate easeDel;
    public NumericValue targetValue;
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