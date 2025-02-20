using System.Collections;
using UnityEngine;
public class Projectile_C : Projectile
{
    private float multiplier = 1;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetRandomEnemyPosition());

        StartCoroutine(AnimationManaging());

        base.SetAttack();
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }
        }
    }
    protected override IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime * multiplier;
            multiplier -= Time.deltaTime / so.projectile_Info.speed;

            if(multiplier <= 0)
            {
                moving = null;
                multiplier = 1;

                yield break;
            }

            yield return null;
        }
    }
    private IEnumerator AnimationManaging()
    {
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        anime.Play("default");

        yield return new WaitUntil(() => multiplier <= 0.1f);
        
        anime.Play("destroy");
    }
}