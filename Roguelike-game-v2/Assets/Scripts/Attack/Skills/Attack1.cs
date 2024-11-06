using UnityEngine;
public class Attack1 : Attack
{
    protected override void SetAttack(int level)
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetNearestEnemyPosition();
        Vector3 direction = Calculate.GetDirection(targetPosition);
        Vector3 localSize = Calculate.GetVector(attackSO.attackSize);
        Quaternion quaternion = Calculate.GetQuaternion(direction);

        gameObject.transform.position = Calculate.GetAttackPosition(direction, attackSO.attackRange);
        gameObject.transform.rotation = quaternion;
        gameObject.transform.localScale = localSize;
    }
}