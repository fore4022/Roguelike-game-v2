using System.Collections;
using UnityEngine;
public class ExceptionMonster_A : BasicMonster
{
    [SerializeField]
    private float duration = 2.5f;

    private WaitForSeconds delay;
    private string prefabKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(RepeatBehavior());   
    }
    protected override void Init()
    {
        delay = new(duration);
        prefabKey = monsterSO.extraObject.name;

        base.Init();
    }
    private IEnumerator RepeatBehavior()
    {
        while(true)
        {
            yield return delay;

            if(isVisible)
            {
                Managers.Game.objectPool.ActiveObject(prefabKey);
            }
        }
    }
}