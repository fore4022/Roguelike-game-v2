using System.Collections;
using UnityEngine;
public class BasicMonster : MonoBehaviour, IDamage, IDamageReceiver, IMoveable
{
    private BasicMonsterStat_SO monsterStat;

    public BasicMonsterStat_SO MonsterStat { get { return monsterStat; } }
    public float Damage { get; }
    private void Start()
    {
        StartCoroutine(AwaitLoadStat());
    }
    public void OnMove()
    {
        Vector3 direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        transform.position += direction * monsterStat.moveSpeed * Time.deltaTime;
    }
    public void Die()
    {
        StartCoroutine(Dieing());
    }
    public void GetDamage(IDamage damage)
    {
        monsterStat.health -= damage.Damage;

        if (monsterStat.health <= 0)
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
    private IEnumerator Dieing()
    {
        //animation play

        yield return new WaitForSeconds(monsterStat.dieing_AnimationDuration);

        //ObjectPool.DisableObject(this.gameObject);
    }
}