using System.Collections;
using UnityEngine;
public class MonsterSkill_D : MonsterSkill_Damage
{
    private const float triggerTime = 0.975f;

    protected override void Enable()
    {
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
        SetActive(true);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= triggerTime);

        SetActive(false);
    }
}