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
        gameObject.transform.position = EnemyDetection.GetNearestEnemyPosition();
        gameObject.transform.rotation = Calculate.GetRandomQuaternion();
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}