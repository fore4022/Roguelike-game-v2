using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ���� ū �� ������ �����Ѵ�.
/// </summary>
public class Skill_B : Skill, ISkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = EnemyDetection.GetLargestEnemyGroup() + so.adjustmentPosition;
    }
    public void SetCollider()
    {
        defaultCollider.enabled = false;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}