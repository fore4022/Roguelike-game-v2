using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 플레이어 주변 반경에서 가장 가까운 적을 향해서 생성된다.
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