using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// SlimeSquareH 전용
/// </para>
/// 사망시, 자기 자신을 작아진 형태로 @개 소환한다. 작아진 형태가 사망할 경우에는 동작하지 않는다.
/// </summary>
public class Monster_F : Monster_WithObject
{
    [SerializeField, Min(0.25f)]
    private float defaultScale;
    [SerializeField, Min(0.1f)]
    private float splitScale;
    [SerializeField, Min(2)]
    private float splitInstanceCount;

    private Vector3 _defaultScale;
    private string monsterKey;
    private float adjustmentScale;

    private bool IsSplite { get { return transform.localScale.x == splitScale; } }
    protected override void Init()
    {
        _defaultScale = new(defaultScale, defaultScale);
        monsterKey = monsterSO.extraObjects[0].name;
        adjustmentScale = defaultScale / 20;

        if(!IsSplite)
        {
            transform.localScale = _defaultScale;
        }

        base.Init();
    }
    protected override void SetPosition()
    {
        if(!IsSplite)
        {
            base.SetPosition();
        }
    }
    protected override void Die()
    {
        if(!IsSplite)
        {
            StartCoroutine(RepeatBehavior());
        }
        else
        {
            base.Die();
        }
    }
    private void OnDisable()
    {
        transform.localScale = _defaultScale;
    }
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);

        PoolingObject go;

        for(int i = 0; i < splitInstanceCount; i++)
        {
            go = Managers.Game.objectPool.GetObject(monsterKey);
            go.transform.localScale = new Vector2(splitScale, splitScale);
            go.transform.position = transform.position + new Vector3(adjustmentScale * Random.Range(-1, 2), adjustmentScale * Random.Range(-1, 2));

            go.SetActive(true);

            yield return null;
        }

        base.Die();
    }
}