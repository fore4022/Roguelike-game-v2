using System.Collections;
using UnityEngine;
public class Projectile_A : Projectile
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.calculate.GetDirection(Managers.Game.player.gameObject.transform.position);
        transform.rotation = Managers.Game.calculate.GetQuaternion(direction);
        penetration_count = so.projectile_Info.penetration;
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }

            penetration_count--;
        }
    }
    protected override IEnumerator Attacking()
    {
        moving = StartCoroutine(Moving());

        yield return new WaitUntil(() => moving == null);

        if (so.projectile_Info.animationName != "")
        {
            anime.Play(so.projectile_Info.animationName);

            yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        }

        attack = null;
    }
    protected override IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            if(penetration_count < 0)
            {
                moving = null;

                yield break;
            }

            yield return null;
        }
    }
}