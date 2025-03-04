public class Attack_B : Attack
{
    protected override void SetAttack()
    {
        transform.position = EnemyDetection.GetLargestEnemyGroup();
    }
}