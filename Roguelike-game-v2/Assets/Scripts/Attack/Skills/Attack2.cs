using UnityEngine;
public class Attack2 : Attack
{
    protected override void SetAttack(int level)
    {
        Vector3 targetPosition = EnemyDetection.GetLargestEnemyGroup();

        transform.position = targetPosition;
    }
}
