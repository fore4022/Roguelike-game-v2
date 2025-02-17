using System.Collections;
using UnityEngine;
public class Projectile_B : Projectile
{
    private bool isAnime = false;

    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetNearestEnemyPosition());
        transform.rotation = Managers.Game.calculate.GetQuaternion(direction);
        penetration_count = so.projectile_Info.penetration;
    }
    protected override void Enter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            if(go.TryGetComponent(out IDamageReceiver damageReceiver))
            {
                damageReceiver.TakeDamage(this);
            }

            if(@anime.GetCurrentAnimatorStateInfo(0).IsName("explosion"))
            {
                isAnime = true;

                anime.Play("explosion");
            }
        }
    }
    protected override IEnumerator Moving()
    {
        while (true)
        {
            transform.position += direction * so.projectile_Info.speed * Time.deltaTime;

            if(isAnime)
            {
                if(anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                {
                    moving = null;

                    yield break;
                }
            }

            yield return null;
        }
    }
}