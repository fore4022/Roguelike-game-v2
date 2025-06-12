using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격
/// </para>
/// 대기 애니메이션 재생 이후 적을 공격하며, 화면 상의 무작위 공간에 위치한다.
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