using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// ���Ÿ� ���� / ���� Ÿ�� ���Ÿ� ����
/// </para>
/// ���� ����� ���� ���ؼ� ���ư���.
/// </summary>
public class Projectile_A : ProjectileSkill, IProjectile
{
    private int penetration_count;

    public bool Finished { get { return moving == null; } }
    public void Set()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        transform.rotation = Calculate.GetQuaternion(direction - so.adjustmentRotation);
        penetration_count = so.projectile_Info.penetration;
        moving = StartCoroutine(Moving());
    }
    public void Enter(GameObject go)
    {
        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }

        penetration_count--;

        if(penetration_count == 0)
        {
            StopCoroutine(moving);

            moving = null;
        }
    }
    public IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            yield return null;
        }
    }
}