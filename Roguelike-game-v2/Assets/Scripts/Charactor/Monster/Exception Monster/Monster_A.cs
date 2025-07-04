using System.Collections;
using UnityEngine;
public class Monster_A : Monster_WithObject
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
        PoolingObject go;

        while(true)
        {
            yield return delay;

            if((Managers.Game.player.transform.position - transform.position).magnitude <= Util.CameraHeight / 2)
            {
                go = Managers.Game.objectPool.GetObject(skillKey);
                go.transform.position = transform.position;

                go.SetActive(true);
            }
        }
    }
}