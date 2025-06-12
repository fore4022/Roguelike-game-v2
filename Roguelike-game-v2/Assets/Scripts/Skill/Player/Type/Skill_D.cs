using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���� ����
/// </para>
/// ��� �ִϸ��̼� ��� ���� ���� �����ϸ�, ȭ�� ���� ������ ������ ��ġ�Ѵ�.
/// </summary>
public class Skill_D : Skill, ISkill
{
    [SerializeField]
    private string animationName;

    public bool Finished { get { return anime.GetCurrentAnimatorStateInfo(0).IsName(animationName); } }
    public void Set()
    {
        transform.position = Calculate.GetRandomVector();

        anime.Play("default", 0);

        StartCoroutine(Attacking());
    }
    public void SetCollider()
    {
        defaultCollider.enabled = false;
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    public IEnumerator Attacking()
    {
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        audioSource.Play();
        anime.Play(animationName, 0);
    }
}