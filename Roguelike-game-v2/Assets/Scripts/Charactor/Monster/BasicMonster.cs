using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private BasicMonsterStat_SO Stat { get { return (BasicMonsterStat_SO)stat; } }
    public float Damage { get { return Stat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(Moving());
    }
    public void OnMove()
    {
        Vector3 direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        transform.position += direction * Stat.moveSpeed * Time.deltaTime;
    }
    private void Die()
    {
        stat = null;
        render = null;

        StartCoroutine(Dieing());
    }
    protected override void OnCollisionEnter2D(Collision2D collision)//
    {
        base.OnCollisionEnter2D(collision);

        Managers.Game.player.GetDamage(this);
    }
    public void GetDamage(IDamage damage)
    {
        Stat.health -= damage.Damage;

        if(Stat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Moving()
    {
        stat = ObjectPool.GetScriptableObject<BasicMonsterStat_SO>(name);

        yield return new WaitUntil(() => Stat != null);

        while(true)
        {
            OnMove();

            yield return null;
        }
    }
    private IEnumerator Dieing()
    {
        //animation play

        yield return new WaitForSeconds(Stat.dieing_AnimationDuration);

        ObjectPool.DisableObject(this.gameObject);
    }
}