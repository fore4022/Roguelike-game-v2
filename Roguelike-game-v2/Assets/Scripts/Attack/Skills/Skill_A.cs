using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 가장 가까운 적을 공격하며, 원의 외각에 위치한다.
/// </summary>
public class Skill_A : Skill, Iskill
{
    private Vector3 direction;

    public bool Finished { get { return true; } }
    public void SetAttack()
    {
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        gameObject.transform.position = Calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = Calculate.GetQuaternion(direction, so.baseRotation);
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