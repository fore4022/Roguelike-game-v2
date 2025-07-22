using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ڽ��� �ֺ��� ���͸� �����Ѵ�.
/// </summary>
public class Monster_I : Monster_WithObject
{
    [SerializeField]
    private List<Vector3> skillOffset;
    [SerializeField]
    private float coolTime;
    [SerializeField]
    private float monsterCount;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string monsterKey;

    protected override void Init()
    {
        delay = new(coolTime);
        monsterKey = monsterSO.extraObjects[0].name;

        base.Init();
    }
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
        if(Random.Range(0, 2) == 1)
        {
            yield return delay;
        }

        PoolingObject go;

        while(true)
        {
            for(int i = 0; i < monsterCount; i++)
            {
                go = Managers.Game.objectPool.GetObject(monsterKey);
                go.Transform.position = transform.position + skillOffset[i];
                
                go.SetActive(true);
            }

            yield return delay;
        }
    }
}