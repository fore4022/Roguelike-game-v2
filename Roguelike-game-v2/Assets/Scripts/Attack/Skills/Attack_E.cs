using UnityEngine;
public class Attack_E : Attack
{
    [SerializeField]
    private int min_Index;
    [SerializeField]
    private int max_Index;

    protected override void SetAttack()
    {
        int rand = Random.Range(min_Index, max_Index + 1);

        transform.position = EnemyDetection.GetLargestEnemyGroup();

        anime.Play($"default_{rand}");
    }
}