using UnityEngine;
public class Attack1 : Attack
{
    protected override void SetAttack()
    {
        Vector3 direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetNearestEnemyPosition());

        gameObject.transform.position = Managers.Game.calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = Managers.Game.calculate.GetQuaternion(direction);
    }
}