using UnityEngine;
public class Attack2 : Attack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetLargestEnemyGroup();

        transform.position = targetPosition;
    }
}
