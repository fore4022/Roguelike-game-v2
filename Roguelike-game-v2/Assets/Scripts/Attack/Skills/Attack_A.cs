using UnityEngine;
public class Attack_A : Attack, IAttacker
{
    private Vector3 direction;

    public bool Finished { get { return so.duration == 0; } }
    public void SetAttack()
    {
        direction = Calculate.GetDirection(EnemyDetection.GetNearestEnemyPosition());
        gameObject.transform.position = Calculate.GetAttackPosition(direction, so.attackRange[level]);
        gameObject.transform.rotation = Calculate.GetQuaternion(direction, so.baseRotation);
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