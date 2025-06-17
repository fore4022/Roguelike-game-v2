using System.Collections;
using UnityEngine;
public class ExceptionMonster_B : ExceptionMonster
{
    [SerializeField]
    private float coolTime = 3f;

    private Coroutine behavior = null;
    private WaitForSeconds delay;
    private string visualizerKey;
    private string skillKey;

    protected override void OnEnable()
    {
        base.OnEnable();

        behavior = StartCoroutine(RepeatBehavior());
    }
    protected override void Init()
    {
        delay = new(coolTime);
        visualizerKey = monsterSO.visualizer.name;
        //skillKey = monsterSO.extraObject.name;

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
        Vector3 position;

        while(true)
        {
            yield return delay;

            if(isVisible)
            {
                go = Managers.Game.objectPool.GetGameObject(visualizerKey);
                position = go.transform.position = Managers.Game.player.transform.position;

                go.SetActive(true);

                yield return new WaitUntil(() => !go.activeSelf);

                go = Managers.Game.objectPool.GetGameObject(skillKey);
                go.transform.position = Managers.Game.player.transform.position;
            }
        }
    }
}