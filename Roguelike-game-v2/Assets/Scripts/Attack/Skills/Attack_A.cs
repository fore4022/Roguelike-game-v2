using UnityEngine;
public class Attack_A : Attack
{
    private Vector3 direction;

    protected override void SetAttack()
    {
        direction = Managers.Game.calculate.GetDirection(Managers.Game.enemyDetection.GetNearestEnemyPosition());
        gameObject.transform.position = Managers.Game.calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = Managers.Game.calculate.GetQuaternion(direction);
    }
}