using System.Collections;
using UnityEngine;
/// <summary>
/// 일정한 확률로 화면의 무작위 위치에 투사체를 떨어트린다.
/// </summary>
public class Monster_B : Monster_WithObject
{
    [SerializeField]
    private float coolTime = 3f;
    [SerializeField, Range(0, 100)]
    private float skillCastChance;

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
        skillKey = monsterSO.extraObjects[0].name;
        visualizerKey = monsterSO.extraObjects[1].name;

        base.Init();
    }
    protected override void Die()
    {
        base.Die();

        StopCoroutine(behavior);
    }
    private IEnumerator RepeatBehavior()
    {
        while(true)
        {
            yield return delay;

            if(Random.Range(0, 100) <= skillCastChance)
            {
                if((Managers.Game.player.transform.position - transform.position).magnitude <= Util.CameraHeight / 2)
                {
                    Util.GetMonoBehaviour().StartCoroutine(SkillCasting());
                }
            }
        }
    }
    private IEnumerator SkillCasting()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(visualizerKey);
        Vector3 position = go.transform.position = EnemyDetection.GetRandomVector();

        go.SetActive(true);

        yield return new WaitUntil(() => !go.activeSelf);

        go = Managers.Game.objectPool.GetObject(skillKey);
        go.transform.position = position;

        go.SetActive(true);
    }
}