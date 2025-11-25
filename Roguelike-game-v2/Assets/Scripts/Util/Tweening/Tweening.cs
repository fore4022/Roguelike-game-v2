using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// Transform 단위로 트윈을 순차적으로 실행
/// </para>
/// 크기, 위치, 회전에 대한 트윈 애니메이션 구현
/// 이어지는 트윈 실행
/// </summary>
public static class Tweening
{
    private delegate void TweenDelegate(Transform transform, NumericValue initial, NumericValue target, float value);

    private static readonly Type _rectTransform = typeof(RectTransform);

    // 트윈 타입에 맞추어서 Transform을 변경
    public static IEnumerator OverTime(TweenType type, TweenData data, Transform transform, EaseDelegate easeDel, NumericValue targetValue, float duration, float delay = 0)
    {
        TweenDelegate del = null;
        TweenStatus status = null;
        NumericValue initialValue = new();

        float currentTime = 0;
        bool isRectTransform = transform.GetType() == _rectTransform;

        yield return new WaitUntil(() => Tween_Manage.GetStatus(transform) != null);

        status = Tween_Manage.GetStatus(transform);

        yield return new WaitForEndOfFrame();

        if(delay > 0)
        {
            while(currentTime < delay)
            {
                if(status.flag)
                {
                    currentTime += Time.deltaTime;
                }

                yield return null;
            }

            currentTime = 0;
        }

        if(isRectTransform)
        {
            switch(type)
            {
                case TweenType.Scale:
                    initialValue.Float = transform.localScale.x;
                    del += Scale_rt;
                    break;
                case TweenType.Position:
                    if(isRectTransform)
                    {
                        initialValue.Vector = (transform as RectTransform).anchoredPosition;
                    }
                    else
                    {
                        initialValue.Vector = transform.localPosition;
                    }

                    del += Position_rt;
                    break;
                case TweenType.Rotation:
                    initialValue.Vector = transform.localRotation.eulerAngles;
                    del += Rotation_rt;
                    break;
            }
        }
        else
        {
            switch(type)
            {
                case TweenType.Scale:
                    initialValue.Float = transform.localScale.x;
                    del += Scale_tf;
                    break;
                case TweenType.Position:
                    initialValue.Vector = transform.localPosition;
                    del += Position_tf;
                    break;
                case TweenType.Rotation:
                    initialValue.Vector = transform.localRotation.eulerAngles;
                    del += Rotation_tf;
                    break;
            }
        }

        if(duration != 0)
        {
            while(currentTime != duration)
            {
                if(status.flag)
                {
                    if(isRectTransform)
                    {
                        currentTime = Mathf.Min(currentTime + Time.unscaledDeltaTime, duration);
                    }
                    else
                    {
                        currentTime = Mathf.Min(currentTime + Time.deltaTime, duration);
                    }

                    del(transform, initialValue, targetValue, easeDel(currentTime / duration));
                }

                yield return null;
            }

            Tween_Manage.Release(transform, data);
        }
        else
        {
            ToEnd(data);
        }
    }
    // 현재 트윈의 완료 상태로 Transform 변경
    public static void ToEnd(TweenData data)
    {
        TweenDelegate del = null;
        NumericValue initialValue = new();

        bool isRectTransform = data.trans.GetType() == _rectTransform;

        if(isRectTransform)
        {
            switch(data.type)
            {
                case TweenType.Scale:
                    initialValue.Float = default;
                    del += Scale_rt;
                    break;
                case TweenType.Position:
                    initialValue.Vector = default;
                    del += Position_rt;
                    break;
                case TweenType.Rotation:
                    initialValue.Vector = default;
                    del += Rotation_rt;
                    break;
            }
        }
        else
        {
            switch(data.type)
            {
                case TweenType.Scale:
                    initialValue.Float = default;
                    del += Scale_tf;
                    break;
                case TweenType.Position:
                    initialValue.Vector = default;
                    del += Position_tf;
                    break;
                case TweenType.Rotation:
                    initialValue.Vector = default;
                    del += Rotation_tf;
                    break;
            }
        }

        if(isRectTransform)
        {
            del(data.trans as RectTransform, initialValue, data.targetValue, data.easeDel(1));
        }
        else
        {
            del(data.trans, initialValue, data.targetValue, data.easeDel(1));
        }

        Tween_Manage.Release(data.trans, data);
    }

    // 객체 크기 변경 트윈 애니메이션
    private static void Scale_tf(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localScale = new Vector2(Mathf.Lerp(initial.Float, target.Float, value), Mathf.Lerp(initial.Float, target.Float, value));
    }
    private static void Scale_rt(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        (transform as RectTransform).localScale = new Vector2(Mathf.Lerp(initial.Float, target.Float, value), Mathf.Lerp(initial.Float, target.Float, value));
    }

    // 객체 위치 변경 트윈 애니메이션
    private static void Position_tf(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.position = Vector3.Lerp(initial.Vector, target.Vector, value);
    }
    private static void Position_rt(Transform transform, NumericValue initial, NumericValue target, float value)
    {        
        (transform as RectTransform).anchoredPosition = Vector3.Lerp(initial.Vector, target.Vector, value);
    }

    // 객체 회전 트윈 애니메이션
    private static void Rotation_tf(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
    private static void Rotation_rt(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
}