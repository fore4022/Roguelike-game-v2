using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ���� ū �� ������ �����ϸ�, ���� �ٸ� �ִϸ��̼��� ����� �� �ִ�.
/// </summary>
public class Skill_E : Skill, ISkill
{
    [SerializeField]
    private int min_Index;
    [SerializeField]
    private int max_Index;

    public bool Finished { get { return true; } }
    public void Set()
    {
        int rand = Random.Range(min_Index, max_Index + 1);

        transform.position = EnemyDetection.GetLargestEnemyGroup();

        animator.Play($"default_{rand}");
    }
    public void SetCollider()
    {
        enable = !enable;
        defaultCollider.enabled = enable;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}