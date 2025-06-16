using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���� ������ ���Ÿ� ����
/// </para>
/// ������ ����� ������ ���ư���, ���� �ð� ���� �������.
/// </summary>
public class Projectile_C : ProjectileSkill, IProjectile
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
    public void Set()
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
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * multiplier * Time.deltaTime;
            multiplier -= Time.deltaTime;

            transform.Rotate(sign * Vector3.back * Time.timeScale);

            if(multiplier <= 0)
            {
                moving = null;

                yield break;
            }

            yield return null;
        }
    }
    private IEnumerator AnimationManaging()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        animator.Play("default");

        yield return new WaitUntil(() => multiplier <= 0.1f);
        
        animator.Play(so.projectile_Info.animationName);
    }
}