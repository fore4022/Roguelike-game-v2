using System.Collections;
using UnityEngine;
public class Projectile_B : Projectile
{
    private bool isExplosion = false;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetNearestEnemyPosition());
        transform.rotation = Managers.Game.calculate.GetQuaternion(direction);
        penetration_count = so.projectile_Info.penetration;

        anime.Play("default");
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }

            if(!isExplosion)
            {
                isExplosion = true;

                anime.Play(so.projectile_Info.animationName);

                StopCoroutine(moving);

                moving = null;
            }
        }
    }
    protected override IEnumerator Attacking()
    {
        StartMove();

        yield return new WaitUntil(() => moving == null);

        yield return base.Attacking();
    }
    protected override IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}