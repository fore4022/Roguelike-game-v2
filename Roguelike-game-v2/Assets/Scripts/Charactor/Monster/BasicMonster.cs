using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private DefaultStat stat;
    private CharactorInformation charactor;
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
        BasicMonsterStat_SO basicMonsterSO = ObjectPool.GetScriptableObject<BasicMonsterStat_SO>(name);

        stat = basicMonsterSO.stat;
        charactor = basicMonsterSO.charactor;

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

        yield return new WaitForSeconds(charactor.dieing_AnimationDuration);

        stat = null;

        ObjectPool.DisableObject(this.gameObject);
    }
}