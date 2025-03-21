using UnityEngine;
public class Attack_E : Attack, IAttacker
{
    [SerializeField]
    private int min_Index;
    [SerializeField]
    private int max_Index;

    public bool Finished { get { return so.duration == 0; } }
    public void SetAttack()
    {
        int rand = Random.Range(min_Index, max_Index + 1);

        transform.position = EnemyDetection.GetLargestEnemyGroup();

        anime.Play($"default_{rand}");
    }
    public void SetCollider()
    {
        enable = !enable;
        defaultCollider.enabled = enable;
    }
    public void Enter(GameObject go)
    {
        if (!go.CompareTag("Monster"))
        {
            return;
        }

        if (go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
}