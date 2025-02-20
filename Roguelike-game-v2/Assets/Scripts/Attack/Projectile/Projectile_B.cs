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
        isExplosion = false;

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
                anime.Play(so.projectile_Info.animationName);

                isExplosion = true;

                StopCoroutine(moving);

                moving = null;
            }
        }
    }
    protected override IEnumerator Attacking()
    {
        StartMove();

        yield return new WaitUntil(() => isExplosion);

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName));

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