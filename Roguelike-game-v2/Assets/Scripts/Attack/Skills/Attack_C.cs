public class Attack_C : Attack
{
    protected override void SetAttack()
    {
        transform.position = Managers.Game.enemyDetection.GetRandomEnemyPosition();
    }
}