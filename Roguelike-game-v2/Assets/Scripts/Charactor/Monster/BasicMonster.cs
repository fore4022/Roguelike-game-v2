using UnityEngine;
public class BasicMonster : MonoBehaviour, IDamage, IDamageReceiver, IMoveable
{
    protected Stat_SO stat;

    public Stat_SO Stat { get { return stat; } }
    public float Damage { get; }
    protected void Start()
    {
        //stat load
    }
    protected void Update()
    {
        OnMove();
    }
    public void OnMove()
    {

    }
    public void Die()
    {

    }
    public void GetDamage(IDamage damage)
    {
        stat.health -= damage.Damage;

        if(stat.health <= 0)
        {
            Die();
        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.GetDamage(this);
        }
    }
}