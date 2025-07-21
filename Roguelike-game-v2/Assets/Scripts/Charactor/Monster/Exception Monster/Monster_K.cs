using System.Collections;
using UnityEngine;
public class Monster_K : BasicMonster
{
    [SerializeField]
    private float dashSpeedMultiplier;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldown;
    [SerializeField]
    private float targetDirectionMultiplier;

    private Coroutine behavior = null;
    private WaitForSeconds delay;

    protected override void Init()
    {
        base.Init();

        delay = new(dashDuration);
    }
    protected override void Enable()
    {
        base.Enable();
    
        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Die()
    {
        base.Die();

        canSwitchDirection = true;

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        canSwitchDirection = true;

        yield return new WaitForSeconds(Random.Range(dashCooldown / 2, dashCooldown));

        float totalTime = 0;

        canSwitchDirection = false;

        while(totalTime != dashDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > dashDuration)
            {
                totalTime = dashDuration;
            }

            speedMultiplier = Mathf.Lerp(speedMultiplierDefault, dashSpeedMultiplier, totalTime / dashDuration);
            directionMultiplier = Mathf.Lerp(directionMultiplierDefault, targetDirectionMultiplier, totalTime / dashDuration);

            yield return null;
        }

        yield return delay;

        StartCoroutine(RepeatBehavior());
    }
}