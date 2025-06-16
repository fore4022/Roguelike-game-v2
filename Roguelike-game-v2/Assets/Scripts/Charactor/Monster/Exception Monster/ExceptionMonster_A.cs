using System.Collections;
using UnityEngine;
public class ExceptionMonster_A : ExceptionMonster
{
    [SerializeField]
    private float duration = 2.5f;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string prefabKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Init()
    {
        delay = new(duration);
        prefabKey = monsterSO.extraObject.name;

        base.Init();
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        GameObject go;

        while(true)
        {
            yield return delay;

            if(isVisible)
            {
                go = Managers.Game.objectPool.GetGameObject(prefabKey);
                go.transform.position = transform.position;

                go.SetActive(true);
            }
        }
    }
}