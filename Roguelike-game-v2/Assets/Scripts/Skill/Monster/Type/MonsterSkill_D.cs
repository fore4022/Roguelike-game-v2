using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CircleCollider2D))]
public class MonsterSkill_D : MonsterSkill_Damage
{
    private const float triggerTime = 0.975f;

    protected override void Enable()
    {
        SetActive(true);
        StartCoroutine(Casting());
    }
    protected override void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    private IEnumerator Casting()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= triggerTime);

        gameObject.SetActive(false);
    }
}