using System.Collections;
using UnityEngine;
public class Tween
{
    private delegate void Tween_TF(Transform transform, FlexibleValue initial, FlexibleValue target, float value);
    private delegate void Tween_RTF(RectTransform rectTransform, FlexibleValue initial, FlexibleValue target, float value);

    public IEnumerator OverTime(TweenType type, EaseDelegate ease, Transform transform, FlexibleValue targetValue, float duration)
    {
        Tween_TF del = null;
        FlexibleValue initialValue = new();
        Status status = null;
        float currentTime = 0;

        yield return new WaitUntil(() => TweenSystemManage.GetStatus(transform) != null);

        status = TweenSystemManage.GetStatus(transform);

        yield return new WaitForEndOfFrame();

        switch (type)
        {
            case TweenType.Scale:
                initialValue.Float = transform.localScale.x;
                del += Scale;
                break;
            case TweenType.Position:
                break;
            case TweenType.Rotation:
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

                del(transform, initialValue, targetValue, ease(currentTime / duration));
            }

            yield return null;
        }
    }

    // Scale
    public void Scale(Transform transform, FlexibleValue inital, FlexibleValue target, float value)
    {
        transform.localScale = Calculate.GetVector(Mathf.Lerp(inital.Float, target.Float, value));
    }

    public IEnumerator ScaleOverTime(EaseDelegate ease, RectTransform rectTransform, float targetScale, float duration)
    {
        Vector3 scale = new();
        float initialScale = rectTransform.localScale.x;
        float currentTime = 0;
        float scaleValue;

        while(currentTime != duration)
        {
            currentTime += Time.unscaledDeltaTime;

            if(currentTime > duration)
            {
                currentTime = duration;
            }

            scaleValue = Mathf.Lerp(initialScale, targetScale, ease(currentTime / duration));
            scale.x = scaleValue;
            scale.y = scaleValue;
            rectTransform.localScale = scale;

            yield return null;
        }
    }

    // Position
    public IEnumerator PositionOverTime(EaseDelegate ease, Transform transform, Vector2 targetPosition, float duration)
    {
        Vector3 initialPosition = transform.position;
        float currentTime = 0;

        while(currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if(currentTime > duration)
            {
                currentTime = duration;
            }

            transform.position = Vector3.Lerp(initialPosition, targetPosition, ease(currentTime / duration));

            yield return null;
        }
    }

    // Rotation
    public IEnumerator RotationOverTime(EaseDelegate ease, Transform transform, Vector3 targetRotation, float duration)
    {
        float currentTime = 0;

        while(currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if(currentTime > duration)
            {
                currentTime = duration;
            }

            transform.rotation = Quaternion.Euler(targetRotation * ease(currentTime / duration));

            yield return null;
        }
    }
}