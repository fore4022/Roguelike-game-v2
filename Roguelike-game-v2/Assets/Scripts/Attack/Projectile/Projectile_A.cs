using System.Collections;
using UnityEngine;
public class Projectile_A : Projectile
{
    private int penetration_count;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        transform.rotation = Calculate.GetQuaternion(direction - so.baseRotation);
        penetration_count = so.projectile_Info.penetration;

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

            penetration_count--;
        }
    }
    protected override IEnumerator Moving()
    {
        while(true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            if(penetration_count <= 0)
            {
                moving = null;

                yield break;
            }

            yield return null;
        }
    }
}