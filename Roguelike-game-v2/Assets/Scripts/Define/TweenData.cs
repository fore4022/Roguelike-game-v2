using UnityEngine;
/// <summary>
/// <para>
/// Tween에 대한 정보를 담은 타입
/// </para>
/// Ease 타입, Ease를 담은 Delegate, Tween의 대상이 되는 transform, 코루틴, 지속시간이 있다.
/// </summary>
public class TweenData
{
    public TweenType type;
    public NumericValue targetValue;
    public EaseDelegate easeDel;
    public Transform trans;

    public Coroutine coroutine;
    public float duration;

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