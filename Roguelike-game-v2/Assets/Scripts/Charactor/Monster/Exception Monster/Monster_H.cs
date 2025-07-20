using System.Collections;
using UnityEngine;
/// <summary>
/// 일정한 확률로 플레이어에게 돌진한다. 돌진이 끝난 후, 현재 위치에서 스킬을 시전한다.
/// </summary>
public class Monster_H : Monster_WithObject
{
    [SerializeField]
    private string skillAnimation_Name;
    [SerializeField]
    private float skillDuration;
    [SerializeField]
    private float skillCooldown;
    [SerializeField, Range(0, 100)]
    private float skillCastChance;
    [SerializeField]
    private float skillDelay;

    private const string defaultAnimation_Name = "Walk";
    private readonly Vector3 skillPosition = new(0.215f, 0);

    private WaitForSeconds duration;
    private WaitForSeconds cooldown;
    private WaitForSeconds delay;
    private string skillKey;
    private bool isEnterPlayer = false;

    protected override void Enable()
    {
        base.Enable();

        StartCoroutine(RepeatBehavior());
    }
    protected override void Attack()
    {
        base.Attack();

        isEnterPlayer = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isEnterPlayer = false;
    }
    protected override void Init()
    {
        duration = new(skillDuration);
        cooldown = new(skillCooldown);
        delay = new(skillDelay);
        skillKey = monsterSO.extraObjects[0].name;

        base.Init();
    }
    private IEnumerator RepeatBehavior()
    {
        animator.Play(defaultAnimation_Name);

        speedMultiplier = 1;
        canSwitchDirection = true;

        yield return cooldown;

        if(Random.Range(0, 100) <= skillCastChance)
        {            
            speedMultiplier = 0;
            canSwitchDirection = false;

            yield return delay;

            float totalTime = 0;

            animator.speed = 0;
            speedMultiplier = 3;

            while(totalTime != skillDuration)
            {
                totalTime += Time.deltaTime;

                if(totalTime > skillDuration)
                {
                    totalTime = skillDuration;
                }

                if(isEnterPlayer)
                {
                    break;
                }

                yield return null;
            }

            if(isVisible)
            {
                PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

                float sign = render.flipX ? 1 : -1;

                animator.speed = 1;
                speedMultiplier = 0;
                go.transform.position = transform.position + skillPosition * sign;
                go.transform.localScale = new(sign, 1, 1);

                go.SetActive(true);
                animator.Play(skillAnimation_Name);

                yield return new WaitUntil(() => !go.activeSelf);
            }
        }

        StartCoroutine(RepeatBehavior());
    }
}