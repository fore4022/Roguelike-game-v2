using UnityEngine;
public class TweenData
{
    public Coroutine coroutine;
    public TweeningType type;
    public Transform trans;
    public EaseDelegate easeDel;
    public NumericValue numeric;
    public float duration;

    public TweenData(Coroutine coroutine)
    {
        this.coroutine = coroutine;
    }
    public TweenData(TweeningType type, Transform trans, EaseDelegate easeDel, NumericValue numeric, float duration)
    {
        this.type = type;
        this.trans = trans;
        this.easeDel = easeDel;
        this.numeric = numeric;
        this.duration = duration;
    }
}