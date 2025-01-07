using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private Coroutine moveCoroutine = null;

    private Color color = new();

    private float totalTime;
    private float colorValue;

    public float DamageAmount { get { return stat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        moveCoroutine = StartCoroutine(Moving());
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

        health -= damage.DamageAmount;

        if(health <= 0)
        {
            Die();
        }
    }
    protected override void Init()
    {
        base.Init();

        totalTime = 0;
    }
    private IEnumerator Moving()
    {
        animator.Play(0, 0);

        while(true)
        {
            OnMove();

            yield return null;
        }
    }
    private IEnumerator Dieing()
    {
        animator.StopRecording();

        while(totalTime != stat.death_AnimationDuration)
        {
            totalTime += Time.fixedDeltaTime;

            if(totalTime > stat.death_AnimationDuration)
            {
                totalTime = stat.death_AnimationDuration;
            }

            colorValue = Mathf.Lerp(1, 0, (totalTime / stat.death_AnimationDuration));

            color.r = color.g = color.b = color.a = colorValue;

            render.color = color;

            yield return null;
        }

        stat = null;

        Managers.Game.inGameData.dataInit.objectPool.DisableObject(gameObject);
    }
}