using System.Collections;
using UnityEngine;
public class BasicMonster : MonoBehaviour, IDamage, IDamageReceiver, IMoveable
{
    private Stat_SO stat;

    public Stat_SO Stat { get { return stat; } }
    public float Damage { get; }
    private void Start()
    {
        StartCoroutine(AwaitLoadStat());
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.GetDamage(this);
        }
    }
    private IEnumerator AwaitLoadStat()
    {
        yield return new WaitUntil(() => stat != null);

        while(true)
        {
            OnMove();

            yield return null;
        }
    }
}