using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ������ ���Ÿ� ����
/// </para>
/// ���� ����� ���� ���ؼ� ���ư���, �浹 �� ���� �� ������ ���ظ� ������.
/// </summary>
public class Projectile_B : ProjectileSkill, IProjectile
{
    [SerializeField]
    private Collider2D effectCollider;

    private bool isExplosion = false;

    public bool Finished { get { return isExplosion && animator.GetCurrentAnimatorStateInfo(0).IsName(so.projectile_Info.animationName); } }
    public void Set()
    {
        animator.Play("default");

        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(MonsterDetection.GetNearestMonsterPosition());
        transform.rotation = Calculate.GetQuaternion(direction);
        moving = StartCoroutine(Moving());
    }
    public void SetCollider()
    {
        if(isExplosion)
        {
            effectCollider.enabled = false;
            defaultCollider.enabled = false;
        }
        else
        {
            effectCollider.enabled = true;
            defaultCollider.enabled = false;
        }
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        if(!isExplosion)
        {
            animator.Play(so.projectile_Info.animationName);
            StopCoroutine(moving);

            moving = null;
            isExplosion = true;
        }
    }
    private void OnDisable()
    {
        effectCollider.enabled = false;
        isExplosion = false;
    }
    public IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}