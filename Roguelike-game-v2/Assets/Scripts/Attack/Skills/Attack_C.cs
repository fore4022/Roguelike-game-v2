using UnityEngine;
public class Attack_C : Attack, IAttacker
{
    public bool Finished { get { return so.duration == 0; } }
    public void SetAttack()
    {
        transform.position = EnemyDetection.GetRandomEnemyPosition();
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