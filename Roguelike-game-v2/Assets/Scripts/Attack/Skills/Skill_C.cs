using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 화면에 보이는 무작위 적을 공격한다.
/// </summary>
public class Skill_C : Skill, ISkill
{
    public bool Finished { get { return true; } }
    public void SetAttack()
    {
        transform.position = EnemyDetection.GetRandomEnemyPosition();
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