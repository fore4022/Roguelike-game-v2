using UnityEngine;
public class Attack_A : Attack
{
    private Vector3 direction;

    protected override void SetAttack()
    {
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        gameObject.transform.position = Calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = Calculate.GetQuaternion(direction, so.baseRotation);
    }
}