using System.Collections;
using UnityEngine;
public class ExceptionMonster_A : ExceptionMonster
{
    [SerializeField]
    private float coolTime = 2.5f;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Init()
    {
        delay = new(coolTime);
        skillKey = monsterSO.extraObject.name;

        base.Init();
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        GameObject skill;

        while(true)
        {
            yield return delay;

            if(isVisible)
            {
                skill = Managers.Game.objectPool.GetGameObject(skillKey);
                skill.transform.position = transform.position;

                skill.SetActive(true);
            }
        }
    }
}