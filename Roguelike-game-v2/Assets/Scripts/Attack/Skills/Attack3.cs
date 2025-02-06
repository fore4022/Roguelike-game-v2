using UnityEngine;
public class Attack3 : Attack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetNearestEnemyPosition();

        transform.position = targetPosition;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.TryGetComponent<Monster>(out Monster monster))
        {
            //monster.
        }
    }
}
