using System.Collections;
using UnityEngine;
public static class ObjectManipulator
{
    public static IEnumerator SetScale(Transform transform, float targetScale, float duration = 0)
    {
        if(duration == 0)
        {
            transform.localScale = new Vector2(targetScale, targetScale);

            yield return null;
        }
        else
        {
            Vector3 scale = new();
            float totalTime = 0;
            float scaleValue;

            while(totalTime != 1)
            {
                totalTime += Time.deltaTime;

                if(totalTime > duration)
                {
                    totalTime = 1;
                }

                scaleValue = Mathf.Lerp(transform.localScale.x, targetScale, totalTime);
                scale.x = scaleValue;
                scale.y = scaleValue;
                transform.localScale = scale;

                yield return null;
            }
        }
    }
    public static IEnumerator TrasnformPosition(Transform transform, Vector2 targetPosition, float duration = 0)
    {
        if(duration == 0)
        {
            transform.position = targetPosition;

            yield return null;
        }
        else
        {
            float totalTime = 0;

            while(totalTime != duration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > duration)
                {
                    totalTime = duration;
                }

                transform.position = Vector3.Lerp(transform.position, targetPosition, totalTime / duration);

                yield return null;
            }
        }
    }
    public static IEnumerator TransformRoatation(Transform transform, Vector2 targetRoatation, float duration = 0)
    {
        targetRoatation = targetRoatation.normalized;

        if(duration == 0)
        {
            transform.rotation = Quaternion.Euler(targetRoatation);

            yield return null;
        }
        else
        {
            float totalTIme = 0;

            while(totalTIme != duration)
            {
                totalTIme += Time.deltaTime;

                if(totalTIme > duration)
                {
                    totalTIme = duration;
                }

                transform.rotation = Quaternion.Euler(targetRoatation * (totalTIme / duration));

                yield return null;
            }
        }
    }
}