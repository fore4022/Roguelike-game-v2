using System.Collections;
using UnityEngine;
public class Attack_D : Attack, IAttacker
{
    [SerializeField]
    private string animationName;

    public bool Finished { get { return anime.GetCurrentAnimatorStateInfo(0).IsName(animationName); } }
    public void SetAttack()
    {
        transform.position = EnemyDetection.GetRandomVector();

        anime.Play("default", 0);

        StartCoroutine(Attacking());
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
    public IEnumerator Attacking()
    {
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        anime.Play(animationName, 0);
    }
}