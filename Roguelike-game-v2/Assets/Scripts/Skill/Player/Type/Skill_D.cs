using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ��� �ִϸ��̼� ��� ���� ���� �����ϸ�, ȭ�� ���� ������ ���� �����Ѵ�.
/// </summary>
public class Skill_D : Skill, ISkill
{
    [SerializeField]
    private string animationName;

    public bool Finished { get { return animator.GetCurrentAnimatorStateInfo(0).IsName(animationName); } }
    public void Set()
    {
        transform.position = MonsterDetection.GetRandomMonsterPosition();

        animator.Play("default", 0);
        StartCoroutine(Attacking());
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
    private IEnumerator Attacking()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        audioSource.Play();
        animator.Play(animationName, 0);
    }
}