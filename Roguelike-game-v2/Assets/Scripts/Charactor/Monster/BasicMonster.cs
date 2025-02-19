using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private const float damagedDuration = 0.15f;

    private Coroutine moveCoroutine = null;
    private WaitForSeconds damaged = new(damagedDuration);
    private Color defaultColor;
    private float animationDuration;

    public float DamageAmount { get { return stat.damage; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        render.color = defaultColor;
        moveCoroutine = StartCoroutine(Moving());
    }
    public void OnMove()
    {
        Vector3 direction = Managers.Game.calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);

        rigid.velocity = direction * stat.moveSpeed;

        if(isVisible)
        {
            animator.speed = 1;

            changeDirection();
        }
        else
        {
            animator.speed = 0;
        }
    }
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
    }
    private void Die()
    {
        Managers.Game.inGameData.player.Experience += experience;

        StopCoroutine(moveCoroutine);

        rigid.simulated = false;

        StartCoroutine(Dieing());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.TakeDamage(this);
        }
    }
    public void TakeDamage(IDamage damage)
    {
        if(!rigid.simulated) 
        {
            return;
        }

        health -= damage.DamageAmount;

        Damaged();

        if(health <= 0)
        {
            Die();
        }
    }
    protected override void Init()
    {
        base.Init();

        defaultColor = render.color;
        animationDuration = stat.death_AnimationDuration;
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
    private IEnumerator TakingDamage()
    {
        render.material.SetFloat("_Float", 1);

        yield return damaged;

        render.material.SetFloat("_Float", 0);
    }
    private IEnumerator Dieing()
    {
        animator.speed = 0;

        StartCoroutine(ColorLerp.ChangeColor(render, Color.black, defaultColor, animationDuration / 2));

        yield return new WaitForSeconds(animationDuration / 2);

        StartCoroutine(ColorLerp.ChangeAlpha(render, 0, render.color.a, animationDuration));

        yield return new WaitForSeconds(animationDuration);

        render.color = defaultColor;

        Managers.Game.inGameData.init.objectPool.DisableObject(gameObject);
    }
}