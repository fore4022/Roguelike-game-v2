using System.Collections;
using UnityEngine;
public class Projectile_1 : Projectile
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.player.gameObject.transform.position;

        direction = (Managers.Game.enemyDetection.FindNearestEnemy().transform.position - transform.position).normalized * Time.deltaTime;
    }
    protected override IEnumerator Attacking()
    {
        yield return null;
    }
    protected override IEnumerator Moving()
    {
        while(true)
        {
            //transform.position += 

            yield return null;
        }
    }
    protected override IEnumerator StartAttack()
    {
        yield return null;
    }
}