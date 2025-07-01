using System.Collections;
using UnityEngine;
public class Monster_B : Monster_WithObject
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
        Vector3 position;

        while(true)
        {
            yield return delay;

            if((Managers.Game.player.transform.position - transform.position).magnitude <= Util.CameraHeight / 2)
            {
                go = Managers.Game.objectPool.GetObject(visualizerKey);
                position = go.transform.position = Managers.Game.player.transform.position + new Vector3(0, 1 / Managers.Game.player.transform.localScale.x);

                go.SetActive(true);

                yield return new WaitUntil(() => !go.activeSelf);

                go = Managers.Game.objectPool.GetObject(skillKey);
                go.transform.position = position;

                go.SetActive(true);
            }
        }
    }
}