using System.Collections;
using UnityEngine;
/// <summary>
/// 수명동안 플레이어를 향해 움직인다. 죽는 효과가 재생되는 동안에 플레이어에게 피해를 입힐 수 있다.
/// </summary>
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