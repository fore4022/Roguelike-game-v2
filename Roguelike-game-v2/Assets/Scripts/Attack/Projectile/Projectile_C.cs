using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 범위 공격형 원거리 공격
/// </para>
/// 무작위 방향과 힘으로 날아가며, 지속 시간 이후 사라진다.
/// </summary>
public class Projectile_C : Projectile, IProjectile
{
    [SerializeField]
    private float range;
    [SerializeField]
    private float min_Index;
    [SerializeField]
    private float max_Index;

    private float multiplier;
    private int sign;

    public bool Finished { get { return moving == null; } }
    public void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(Calculate.GetRandomVector());
        multiplier = Random.Range(min_Index, max_Index + 1) * range + range;
        sign = Random.Range(0, 2);
        moving = StartCoroutine(Moving());

        if(sign == 0)
        {
            sign = -1;
        }

        StartCoroutine(AnimationManaging());
    }
    public void SetCollider()
    {
        enable = !enable;
        defaultCollider.enabled = enable;
    }
    public void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }
        }
    }
    public IEnumerator Moving()
    {
        while (true)
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