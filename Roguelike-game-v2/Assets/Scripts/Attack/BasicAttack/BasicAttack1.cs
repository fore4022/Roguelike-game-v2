using UnityEngine;
public class BasicAttack1 : BaseAttack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = EnemyDetection.GetNearestEnemyPosition();
        Vector3 direction = Calculate.GetDirection(targetPosition);
        Vector3 localSize = Calculate.GetVector(basicAttackSO.attackSize);
        Quaternion quaternion = Calculate.GetQuaternion(direction);

        gameObject.transform.position = Calculate.GetAttackPosition(direction, basicAttackSO.attackRange);
        gameObject.transform.rotation = quaternion;
        gameObject.transform.localScale = localSize;
    }
}
