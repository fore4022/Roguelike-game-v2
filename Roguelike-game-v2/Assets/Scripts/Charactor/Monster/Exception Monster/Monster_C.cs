using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// SnakeF 전용
/// </para>
/// 짧게 끊어서 플레이어를 향해 돌진
/// </summary>
public class Monster_C : BasicMonster
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldownMax;
    [SerializeField]
    private float dashCastingTime;

    protected override void Enable()
    {
        base.Enable();

        speedMultiplier = speedMultiplierDefault;

        StartCoroutine(RepeatBehavior());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitForSeconds(Random.Range(0, dashCooldownMax));

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

        StartCoroutine(RepeatBehavior());
    }
}