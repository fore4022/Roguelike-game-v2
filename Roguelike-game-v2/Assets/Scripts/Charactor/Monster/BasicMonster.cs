using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private BasicMonsterStat_SO stat;
    private Coroutine moveCoroutine = null;

    public float DamageAmount { get { return stat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        moveCoroutine = StartCoroutine(Moving());
    }
    public void OnMove()
    {
        Vector3 direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        transform.position += direction * stat.moveSpeed * Time.deltaTime;
    }
    private void Die()
    {
        rigid.simulated = false;

        render = null;
        rigid = null;

        StopCoroutine(moveCoroutine);

        StartCoroutine(Dieing());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.GetDamage(this);
        }
    }
    public void GetDamage(IDamage damage)
    {
        stat.health -= damage.DamageAmount;

        if(stat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Moving()
    {
        stat = ObjectPool.GetScriptableObject<BasicMonsterStat_SO>(name);

        yield return new WaitUntil(() => stat != null);

        while(true)
        {
            OnMove();

            yield return null;
        }
    }
    private IEnumerator Dieing()
    {
        //animation play

        yield return new WaitForSeconds(stat.dieing_AnimationDuration);

        stat = null;

        ObjectPool.DisableObject(this.gameObject);
    }
}