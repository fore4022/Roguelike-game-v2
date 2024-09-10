using System.Collections;
using UnityEngine;
public class BasicMonster : MonoBehaviour, IDamage, IDamageReceiver, IMoveable
{
    private BasicMonsterStat_SO monsterStat;

    public Define.Stat DefaultStat { get { return monsterStat.stat; } }
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
        monsterStat.stat.health -= damage.Damage;

        if (monsterStat.stat.health <= 0)
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
        yield return new WaitUntil(() => monsterStat != null);

        while(true)
        {
            OnMove();

            yield return null;
        }
    }
}