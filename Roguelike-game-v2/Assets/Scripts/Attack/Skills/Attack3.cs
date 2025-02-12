using UnityEngine;
public class Attack3 : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetRandomEnemyPosition();
    }
}
