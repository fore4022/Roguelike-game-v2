using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private DefaultStat stat;
    private CharactorInformation charactor;
    private Coroutine moveCoroutine = null;

    private float experience;

    public float DamageAmount { get { return stat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        LoadStat();
    }
    public void OnMove()
    {
        Vector3 direction = Managers.Game.calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        rigid.velocity = direction * stat.moveSpeed;
    }
    private void Die()
    {
        Managers.Game.inGameData.playerData.Experience += experience;

        StopCoroutine(moveCoroutine);

        rigid.simulated = false;

        StartCoroutine(Dieing());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.GetDamage(this);
        }
    }
    public void GetDamage(IDamage damage)
    {
        if (!rigid.simulated) { return; }

        stat.health -= damage.DamageAmount;

        if(stat.health <= 0)
        {
            Die();
        }
    }
    private void LoadStat()
    {
        stat = monsterSO.stat;
        charactor = monsterSO.charactor;
        experience = monsterSO.experience;

        moveCoroutine = StartCoroutine(Moving());
    }
    private IEnumerator Moving()
    {
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

        Managers.Game.objectPool.DisableObject(gameObject);
    }
}