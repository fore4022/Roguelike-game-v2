using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// SlimeSquareH ����
/// </para>
/// ����� ��� �۾��� ��ü�� �п��ϸ�, �п��� ��ü�� �߰� �п����� ����
/// �п��� ��ü�� ����ġ�� �������� ����
/// </summary>
public class Monster_F : BasicMonster_WithObject
{
    [SerializeField, Min(0.25f)]
    private float defaultScale;
    [SerializeField, Min(0.1f)]
    private float splitScale;
    [SerializeField, Min(2)]
    private float splitInstanceCount;
    [SerializeField, Range(0, 100)]
    private float skillCastChance;

    private Vector3 _defaultScale;
    private string monsterKey;
    private float adjustmentScale;

    private bool IsSplite { get { return transform.localScale.x == splitScale; } }
    // ũ�� �� �� ���� Ű �ʱ�ȭ
    protected override void Init()
    {
        transform.localScale = _defaultScale = new(defaultScale, defaultScale);
        monsterKey = monsterSO.extraObjects[0].name;
        adjustmentScale = defaultScale / 20;

        base.Init();
    }
    // �п����� ���� ��ü ��ġ ����
    protected override void SetPosition()
    {
        if(!IsSplite)
        {
            base.SetPosition();
        }
    }
    // �п��� ��ü�� ��� FlipX ����
    protected override void Enable()
    {
        if(IsSplite)
        {
            FlipX();
        }

        base.Enable();
    }
    // �п����� ���� ��ü �����, Ȯ�������� �п�
    protected override void Die()
    {
        if(!IsSplite)
        {
            if(Random.Range(0, 100) <= skillCastChance)
            {
                user_Experience = monsterSO.user_Experience;

                StartCoroutine(RepeatBehavior());

                return;
            }
        }
        else
        {
            user_Experience = 0;
        }

        base.Die();
    }
    // �п��� ��� ���� �⺻ ũ��� ����
    private void OnDisable()
    {
        transform.localScale = _defaultScale;
    }
    // ��� ȿ���� Ư�� �������� ���, ���� Ű�� ���ؼ� ũ��� ����ġ�� �缳���� �п��� ��ü ����
    private IEnumerator RepeatBehavior()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f);

        PoolingObject go;

        for(int i = 0; i < splitInstanceCount; i++)
        {
            go = Managers.Game.objectPool.GetObject(monsterKey);
            go.Transform.localScale = new Vector2(splitScale, splitScale);
            go.Transform.position = transform.position + new Vector3(adjustmentScale * Random.Range(-1, 2), adjustmentScale * Random.Range(-1, 2));

            go.SetActive(true);

            yield return null;
        }

        base.Die();
    }
}