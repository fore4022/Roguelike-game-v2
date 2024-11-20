using UnityEngine;
public class Attack1 : Attack
{
    protected override void SetAttack(int level)
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetNearestEnemyPosition();
        Vector3 direction = Managers.Game.calculate.GetDirection(targetPosition);
        Quaternion quaternion = Managers.Game.calculate.GetQuaternion(direction);

        gameObject.transform.position = Managers.Game.calculate.GetAttackPosition(direction, attackSO.attackRange);
        gameObject.transform.rotation = quaternion;
    }
}