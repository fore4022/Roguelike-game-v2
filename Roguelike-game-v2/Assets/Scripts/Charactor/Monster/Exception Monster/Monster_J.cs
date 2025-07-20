using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class Monster_J : BasicMonster
{
    [SerializeField]
    private float lifeTime;

    private WaitForSeconds delay;

    protected override void Init()
    {
        delay = new(lifeTime);

        base.Init();
    }
    protected override void Enable()
    {
        speedMultiplier = speedMultiplierDefault;

        changeDirection();
        Set();
        StartCoroutine(RepeatBehavior());
    }
    protected override void Die()
    {
        StartCoroutine(GradualSlowdown());
        StartCoroutine(Dieing());
    }
    private IEnumerator RepeatBehavior()
    {
        yield return delay;

        Die();
    }
    private IEnumerator GradualSlowdown()
    {
        float totalTime = 0;

        while(totalTime != death_AnimationDuration)
        {
            totalTime += Time.deltaTime;

            if(totalTime > death_AnimationDuration)
            {
                totalTime = death_AnimationDuration;
            }

            speedMultiplier = Mathf.Lerp(0, speedMultiplierDefault, Ease.InCirc(totalTime / death_AnimationDuration));

            yield return null;
        }
    }
}