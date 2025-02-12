using UnityEngine;
public class Attack2 : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetLargestEnemyGroup();
    }
}
