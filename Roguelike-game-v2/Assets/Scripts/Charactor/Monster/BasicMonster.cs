using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private BasicMonsterStat_SO monsterStat;//

    public float Damage { get { return monsterStat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(Moving());
    }
    public void OnMove()
    {
        Vector3 direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        transform.position += direction * monsterStat.moveSpeed * Time.deltaTime;
    }
    protected override void Die()
    {
        base.Die();

        StartCoroutine(Dieing());
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        Managers.Game.player.GetDamage(this);
    }
    public void GetDamage(IDamage damage)
    {
        monsterStat.health -= damage.Damage;

        if (monsterStat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Moving()
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

        ObjectPool.DisableObject(this.gameObject);
    }
}