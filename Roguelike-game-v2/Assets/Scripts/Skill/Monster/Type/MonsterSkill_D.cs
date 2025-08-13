using System.Collections;
using UnityEngine;
public class MonsterSkill_D : MonsterSkill_Damage
{
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
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= Util.triggerTime);

        gameObject.SetActive(false);
    }
}