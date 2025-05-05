using System;
using System.Collections;
using UnityEngine;
public static class Tweening
{
    private delegate void Tween_TF(Transform transform, NumericValue initial, NumericValue target, float value);
    private delegate void Tween_RTF(RectTransform rectTransform, NumericValue initial, NumericValue target, float value);

    private static readonly Type _rectTransform = typeof(RectTransform);

    public static IEnumerator OverTime(TweenType type, TweenData data, Transform transform, EaseDelegate ease, NumericValue targetValue, float duration)
    {
        Tween_TF del = null;
        NumericValue initialValue = new();
        TweenStatus status = null;
        float currentTime = 0;
        bool isRectTransform = transform.GetType() == _rectTransform;

        yield return new WaitUntil(() => TweenSystemManage.GetStatus(transform) != null);

        status = TweenSystemManage.GetStatus(transform);

        yield return new WaitForEndOfFrame();

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

        while(currentTime != duration)
        {
            if(status.flag)
            {
                currentTime += Time.deltaTime;

                if(currentTime > duration)
                {
                    currentTime = duration;
                }

                if(isRectTransform)
                {
                    del(transform as RectTransform, initialValue, targetValue, ease(currentTime / duration));
                }
                else
                {
                    del(transform, initialValue, targetValue, ease(currentTime / duration));
                }
            }

            yield return null;
        }

        TweenSystemManage.Release(transform, data);
    }

    // Scale
    public static void Scale(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localScale = Calculate.GetVector(Mathf.Lerp(initial.Float, target.Float, value));
    }
    public static void Scale(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localScale = Calculate.GetVector(Mathf.Lerp(initial.Float, target.Float, value));
    }

    // Position
    public static void Position(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localPosition = Vector3.Lerp(initial.Vector, target.Vector, value);
    }
    public static void Position(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localPosition = Vector3.Lerp(initial.Vector, target.Vector, value);
    }

    // Rotation
    public static void Rotation(Transform transform, NumericValue initial, NumericValue target, float value)
    {
        transform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
    public static void Rotation(RectTransform rectTransform, NumericValue initial, NumericValue target, float value)
    {
        rectTransform.localRotation = Quaternion.Euler(initial.Vector + target.Vector * value);
    }
}