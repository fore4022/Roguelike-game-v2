using System.Collections;
using UnityEngine;
public static class Tween
{
    private static Easing easing = new();

    public static Transform SetScale(this Transform transform, float targetScale, float duration)
    {
        Vector2 scale = new();
        float currentTime = 0;
        float scaleValue = 0;

        while(currentTime != duration)
        {
            currentTime += Time.deltaTime;

            if(currentTime > duration)
            {
                currentTime = duration;
            }

            //scaleValue = easing.

            //yield return null;
        }

        return transform;
    }
    //private static IEnumerator RunProgress()
    //{
    //    while()
    //    {

    //    }
    //}
}