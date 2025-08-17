using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ���� ����� ���� �����Ѵ�.
/// </summary>
public class Skill_A : Skill, ISkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = EnemyDetection.GetNearestEnemyPosition();
        transform.rotation = Calculate.GetRandomQuaternion();
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}