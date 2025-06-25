using System.Collections;
using UnityEngine;
public class ExceptionMonster_C : ExceptionMonster
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldownMax;
    [SerializeField]
    private float dashCastingTime;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitForSeconds(Random.Range(0, dashCooldownMax));
        
        yield return new WaitUntil(() => isVisible);
        
        if(isVisible)
        {
            float totalTime = 0;

            while(totalTime != dashCastingTime)
            {
                totalTime += Time.deltaTime;

                if(totalTime > dashCastingTime)
                {
                    totalTime = dashCastingTime;
                }

                speedMultiplier = Mathf.Lerp(0, speedMultiplierDefault, totalTime / dashCastingTime);

                yield return null;
            }

            totalTime = 0;

            yield return new WaitForSeconds(dashCastingTime / 4);

            while(totalTime != dashDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > dashDuration)
                {
                    totalTime = dashDuration;
                }

                speedMultiplier = Mathf.Lerp(dashSpeed, 0, totalTime / dashDuration);

                yield return null;
            }

            totalTime = 0;

            while(totalTime != dashCastingTime)
            {
                totalTime += Time.deltaTime;

                if(totalTime > dashCastingTime)
                {
                    totalTime = dashCastingTime;
                }

                speedMultiplier = Mathf.Lerp(speedMultiplierDefault, 0, totalTime / dashCastingTime);

                yield return null;
            }
        }

        StartCoroutine(RepeatBehavior());

        yield break;
    }
}