using System.Collections;
using UnityEngine;
public class Projectile_1 : Projectile
{
    protected override void SetAttack()
    {
        direction = (Managers.Game.enemyDetection.GetNearestEnemyPosition() - transform.position).normalized;
        transform.position = Managers.Game.player.gameObject.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
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
        }

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
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