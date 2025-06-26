using System.Collections;
using UnityEngine;
public class Monster_E : Monster_WithObject
{
    [SerializeField]
    private float cooltime = 3;

    private Coroutine behaviour;
    private WaitForSeconds delay;
    private string skillKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        behaviour = StartCoroutine(RepeatBehaviour());

        SetDirection();
    }
    protected override void Init()
    {
        delay = new(cooltime);
        skillKey = monsterSO.extraObject.name;

        base.Init();
    }
    protected override void SetDirection()
    {
        base.SetDirection();

        canSwitchDirection = false;
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behaviour);
    }
    private IEnumerator RepeatBehaviour()
    {
        GameObject skill;

        while(true)
        {
            yield return delay;

            if((Managers.Game.player.transform.position - transform.position).magnitude <= Util.CameraHeight / 2)
            {
                skill = Managers.Game.objectPool.GetGameObject(skillKey);
                skill.transform.position = transform.position;

                skill.SetActive(true);
            }
        }
    }
}