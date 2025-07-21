using System.Collections;
using UnityEngine;
public class Monster_K : BasicMonster
{
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private float dashDuration;
    [SerializeField]
    private float dashCooldown;

    private Coroutine behavior = null;

    protected override void Enable()
    {
        base.Enable();
    
        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitForSeconds(Random.Range(dashCooldown / 2, dashCooldown));

        float totalTime = 0;

        while(totalTime != dashDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > dashDuration)
            {
                totalTime = dashDuration;
            }

            speedMultiplier = Mathf.Lerp(dashSpeed, speedMultiplierDefault, totalTime / dashDuration);
            directionMultiplier = Mathf.Lerp(0, directionMultiplierDefault, totalTime / dashDuration);

            yield return null;
        }

        StartCoroutine(RepeatBehavior());
    }
}