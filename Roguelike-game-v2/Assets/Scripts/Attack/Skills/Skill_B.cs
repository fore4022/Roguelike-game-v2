using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ���� ū �� ������ �����Ѵ�.
/// </summary>
public class Skill_B : Skill, Iskill
{
    public bool Finished { get { return true; } }
    public void SetAttack()
    {
        transform.position = EnemyDetection.GetLargestEnemyGroup();
    }
    public void SetCollider()
    {
        enable = !enable;
        defaultCollider.enabled = enable;
    }
    public void Enter(GameObject go)
    {
        if (!go.CompareTag("Monster"))
        {
            return;
        }

        if (go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}