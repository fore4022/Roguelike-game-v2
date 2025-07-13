using System.Collections;
using UnityEngine;
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

    private WaitForSeconds cooldown;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(RepeatBehavior());
    }
    protected override void Init()
    {
        cooldown = new(skillCooldown);
        delay = new(skillDelay);
        skillKey = monsterSO.extraObject.name;

        base.Init();
    }
    private IEnumerator RepeatBehavior()
    {
        speedMultiplier = 1;
        canSwitchDirection = true;

        yield return cooldown;

        if(Random.Range(0, 100) <= skillCastChance)
        {            
            PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

            canSwitchDirection = false;
            speedMultiplier = 0;

            yield return delay;

            animator.speed = 0;

            speedMultiplier = 3;

            yield return skillDuration;

            animator.Play(skillAnimation_Name);
            go.SetActive(true);

            animator.speed = 1;

            yield return new WaitUntil(() => !go.activeSelf);
        }

        StartCoroutine(RepeatBehavior());
    }
}