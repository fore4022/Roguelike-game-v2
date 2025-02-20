using UnityEngine;
public class Attack_E : Attack
{
    protected override void SetAttack()
    {
        int rand = Random.Range(1, 3);

        transform.position = Managers.Game.enemyDetection.GetLargestEnemyGroup();

        anime.Play($"default_{rand}");
    }
}