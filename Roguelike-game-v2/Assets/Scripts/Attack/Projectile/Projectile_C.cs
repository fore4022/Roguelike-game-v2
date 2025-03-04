using System.Collections;
using UnityEngine;
public class Projectile_C : Projectile
{
    [SerializeField]
    private float range;
    [SerializeField]
    private float min_Index;
    [SerializeField]
    private float max_Index;

    private float multiplier;
    private int sign;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(EnemyDetection.GetRandomVector());
        multiplier = Random.Range(min_Index, max_Index + 1) * range + range;
        sign = Random.Range(0, 2);

        if(sign == 0)
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