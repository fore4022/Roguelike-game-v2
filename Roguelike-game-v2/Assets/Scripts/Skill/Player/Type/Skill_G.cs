using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// �÷��̾� �ֺ� �ݰ濡�� ���� ����� ���� ���ؼ� �����ȴ�.
/// </summary>
public class Skill_G : Skill, ISkill
{
    [SerializeField]
    private float skillRange;

    public bool Finished { get { return true; } }
    public void Set()
    {
        Vector3 direction = EnemyDetection.GetNearestEnemyPosition();

        transform.rotation = Calculate.GetQuaternion(direction);
        transform.position = Calculate.GetAttackPosition(direction, skillRange);
    }
    public void SetCollider()
    {
        defaultCollider.enabled = !defaultCollider.enabled;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}