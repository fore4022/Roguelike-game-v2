public class Attack_C : Attack
{
    protected override void SetAttack()
    {
        transform.position = EnemyDetection.GetRandomEnemyPosition();
    }
}