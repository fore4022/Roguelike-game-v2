using System.Collections;
using UnityEngine;
public class Projectile_C : Projectile
{
    private const float range = 0.5f;

    private float multiplier;
    private int sign;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetRandomVector());
        multiplier = Random.Range(1, 3) * range + range;
        sign = Random.Range(0, 2);

        if (sign == 0)
        {
            sign = -1;
        }

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
            transform.position += direction * so.projectile_Info.speed * multiplier * Time.deltaTime;
            multiplier -= Time.deltaTime;

            transform.Rotate(sign * Vector3.back);

            if (multiplier <= 0)
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