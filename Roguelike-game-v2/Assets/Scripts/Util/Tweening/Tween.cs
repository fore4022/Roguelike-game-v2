using System.Collections;
using UnityEngine;
public delegate float EaseDelegate(float f);
public class Tween
{
    // Transform
    public IEnumerator ScaleOverTime(EaseDelegate ease, Transform transform, float targetScale, float duration)
    {
        Vector2 scale = new();
        float initialScale = transform.localScale.x;
        float currentTime = 0;
        float scaleValue;

        while(currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if(currentTime > duration)
            {
                currentTime = duration;
            }

            scaleValue = Mathf.Lerp(initialScale, targetScale, ease(currentTime / duration));

            scale.x = scaleValue;
            scale.y = scaleValue;
            transform.localScale = scale;

            yield return null;
        }
    }
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