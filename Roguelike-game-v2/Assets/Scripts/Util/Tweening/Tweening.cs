using System;
using System.Collections;
using UnityEngine;
public static class Tweening
{
    private delegate void Tween_TF(Transform transform, NumericValue initial, NumericValue target, float value);
    private delegate void Tween_RTF(RectTransform rectTransform, NumericValue initial, NumericValue target, float value);

    private static readonly Type _rectTransform = typeof(RectTransform);

    public static IEnumerator OverTime(TweenType type, TweenData data, Transform transform, EaseDelegate easeDel, NumericValue targetValue, float duration, float delay = 0)
    {
        Tween_TF del = null;
        NumericValue initialValue = new();
        TweenStatus status = null;
        float currentTime = 0;
        bool isRectTransform = transform.GetType() == _rectTransform;

        yield return new WaitUntil(() => TweenSystemManage.GetStatus(transform) != null);

        status = TweenSystemManage.GetStatus(transform);

        yield return new WaitForEndOfFrame();

        if(delay > 0)
        {
            while(currentTime < delay)
            {
                if (status.flag)
                {
                    currentTime += Time.deltaTime;
                }

                yield return null;
            }

            currentTime = 0;
        }

        switch(type)
        {
            case TweenType.Scale:
                initialValue.Float = transform.localScale.x;
                del += Scale;
                break;
            case TweenType.Position:
                initialValue.Vector = transform.localPosition;
                del += Position;
                break;
            case TweenType.Rotation:
                initialValue.Vector = transform.localRotation.eulerAngles;
                del += Rotation;
                break;
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

                        del(transform as RectTransform, initialValue, targetValue, easeDel(currentTime / duration));
                    }
                    else
                    {
                        currentTime = Mathf.Min(currentTime + Time.deltaTime, duration);

                        del(transform, initialValue, targetValue, easeDel(currentTime / duration));
                    }
                }

                yield return null;
            }

            TweenSystemManage.Release(transform, data);
        }
        else
        {
            ToEnd(data);
        }
    }
    public static void ToEnd(TweenData data)
    {
        Tween_TF del = null;
        NumericValue initialValue = new();
        bool isRectTransform = data.trans.GetType() == _rectTransform;

        switch(data.type)
        {
            case TweenType.Scale:
                initialValue.Float = default;
                del += Scale;
                break;
            case TweenType.Position:
                initialValue.Vector = default;
                del += Position;
                break;
            case TweenType.Rotation:
                initialValue.Vector = default;
                del += Rotation;
                break;
        }

        if(isRectTransform)
        {
            del(data.trans as RectTransform, initialValue, data.targetValue, data.easeDel(1));
        }
        else
        {
            del(data.trans, initialValue, data.targetValue, data.easeDel(1));
        }

        TweenSystemManage.Release(data.trans, data);
    }

    // Scale
    private static void Scale(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localScale = Calculate.GetVector(Mathf.Lerp(initial.Float, target.Float, value));
    }
    private static void Scale(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localScale = Calculate.GetVector(Mathf.Lerp(initial.Float, target.Float, value));
    }

    // Position
    private static void Position(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localPosition = Vector3.Lerp(initial.Vector, target.Vector, value);
    }
    private static void Position(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localPosition = Vector3.Lerp(initial.Vector, target.Vector, value);
    }

    // Rotation
    private static void Rotation(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
    private static void Rotation(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
}