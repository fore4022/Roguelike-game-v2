using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 가장 큰 적 무리를 공격한다.
/// </summary>
public class Skill_B : Skill, ISkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = MonsterDetection.GetLargestMonsterGroup() + so.adjustmentPosition;
    }
    public void SetCollider()
    {
        defaultCollider.enabled = true;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}