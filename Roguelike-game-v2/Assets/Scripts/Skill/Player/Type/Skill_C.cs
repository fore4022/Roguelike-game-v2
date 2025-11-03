using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 화면에 보이는 무작위 적을 공격
/// </summary>
public class Skill_C : PlayerSkill, IPlayerSkill
{
    public bool Finished { get { return true; } }
    public void Set()
    {
        transform.position = MonsterDetection.GetRandomMonsterPosition();
    }
    public void SetCollider()
    {
        playColliderOnEnable = !playColliderOnEnable;
        defaultCollider.enabled = playColliderOnEnable;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}