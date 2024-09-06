using UnityEngine;
public class Monster1 : MonoBehaviour, Monster
{
    private Stat_SO stat;

    public Stat_SO Stat { get { return stat; } }
    public float Damage { get { return stat.damage; } }
    private void Update()
    {
        OnMove();
    }
    public void OnMove()
    {
        Vector2 direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position - transform.position);

        transform.position += (Vector3)direction * stat.moveSpeed * Time.deltaTime;
    }
    public void Die()
    {

    }
    public void GetDamage(IDamage damage)
    {
        Stat.health -= damage.Damage;

        if(stat.health <= 0)
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(collision.gameObject.TryGetComponent<IDamageReceiver>(out IDamageReceiver damageReceiver))
            {
                damageReceiver.GetDamage(this);
            }
        }
    }
}
