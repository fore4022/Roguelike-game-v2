using UnityEngine;
public class Monster : MonoBehaviour, IAttackable, IDamage, IDamageReceiver, IMoveable
{
    protected Stat_SO stat;

    public Stat_SO Stat { get { return stat; } }

    public float Damage => throw new System.NotImplementedException();

    public void Attack()
    {

    }

    public void CancelMove()
    {

    }

    public void Die()
    {

    }

    public void GetDamage(IDamage damage)
    {

    }

    public void OnMove()
    {

    }

    public void StartMove()
    {

    }

    protected virtual void Init()
    {

    }
}
