public class Attack_B : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetLargestEnemyGroup();
    }
}