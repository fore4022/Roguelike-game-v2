using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 가장 큰 적 무리를 공격하며, 서로 다른 애니메이션을 재생할 수 있다.
/// </summary>
public class Skill_E : PlayerSkill, ISkill
{
    [SerializeField]
    private int min_Index;
    [SerializeField]
    private int max_Index;

    public bool Finished { get { return true; } }
    public void Set()
    {
        int rand = Random.Range(min_Index, max_Index + 1);

        transform.position = MonsterDetection.GetLargestMonsterGroup();

        animator.Play($"default_{rand}");
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