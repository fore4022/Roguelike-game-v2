using System.Collections;
using UnityEngine;
public class ObjectManipulator
{
    public IEnumerator SetScale(Transform transform, float targetScale, float duration = 0)
    {
        if(duration == 0)
        {
            transform.localScale = new Vector2(targetScale, targetScale);
        }
        else
        {
            Vector3 scale = new();
            float totalTime = 0;
            float scaleValue;

            while(totalTime != 1)
            {
                totalTime += Time.unscaledDeltaTime;

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

        yield return null;
    }
    public IEnumerator TrasnfromPosition(Transform transform, Vector2 targetPosition, float duration = 0)
    {
        if(duration == 0)
        {
            transform.position = targetPosition;
        }
        else
        {
            float totalTime = 0;
            float scaleValue;

            while(totalTime != duration)
            {
                totalTime += Time.deltaTime;

                if()

                yield return null;
            }
        }

        yield return null;
    }
}