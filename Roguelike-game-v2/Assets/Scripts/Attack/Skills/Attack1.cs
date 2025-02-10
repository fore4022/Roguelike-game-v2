using UnityEngine;
public class Attack1 : Attack
{
    protected override void SetAttack()
    {
        Vector3 targetPosition = Managers.Game.enemyDetection.GetNearestEnemyPosition();
        Vector3 direction = Managers.Game.calculate.GetDirection(targetPosition);
        Quaternion quaternion = Managers.Game.calculate.GetQuaternion(direction);

        gameObject.transform.position = Managers.Game.calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = quaternion;
    }
}