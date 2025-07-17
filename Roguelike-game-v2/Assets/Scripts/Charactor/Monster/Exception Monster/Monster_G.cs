using UnityEngine;
/// <summary>
/// ������ Ȯ���� �����, ���� ��ġ�� ��ų�� �����Ѵ�.
/// </summary>
public class Monster_G : Monster_WithObject
{
    [SerializeField, Range(0, 100)]
    private float skillCastChance;

    private string skillKey;

    // 0, 0.5f

    protected override void Init()
    {
        skillKey = monsterSO.extraObjects[0].name;

        base.Init();
    }
    protected override void Die()
    {
        if(skillCastChance == 100)
        {
            SkillCast();
        }
        else
        {
            if(Random.Range(0, 100) <= skillCastChance)
            {
                SkillCast();
            }
        }

        base.Die();
    }
    private void SkillCast()
    {
        PoolingObject go = Managers.Game.objectPool.GetObject(skillKey);

        go.transform.position = transform.position;

        go.SetActive(true);
    }
}