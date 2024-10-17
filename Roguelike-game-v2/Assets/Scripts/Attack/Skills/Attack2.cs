using UnityEngine;
public class Attack2 : Attack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = EnemyDetection.GetLargestEnemyGroup();

        transform.position = targetPosition;
    }
}
