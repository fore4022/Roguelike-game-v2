using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    private const float damagedDuration = 0.15f;

    private Coroutine moveCoroutine = null;
    private Color defaultColor;

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

        if(visible)
        {
            changeDirection();
        }
    }
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
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
            Managers.Game.player.TakeDamage(this);
        }
    }
    public void TakeDamage(IDamage damage)
    {
        if(!rigid.simulated) { return; }

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

        animator.enabled = true;
        defaultColor = render.color;
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

        yield return new WaitForSeconds(damagedDuration);

        render.material.SetFloat("_Float", 0);
    }
    private IEnumerator Dieing()
    {
        float animationDuration = stat.death_AnimationDuration;

        animator.enabled = false;

        StartCoroutine(ColorLerp.ChangeColor(render, Color.black, defaultColor, animationDuration / 2));

        yield return new WaitForSeconds(animationDuration / 2);

        StartCoroutine(ColorLerp.ChangeAlpha(render, 0, render.color.a, animationDuration));

        yield return new WaitForSeconds(animationDuration);

        render.color = defaultColor;

        Managers.Game.inGameData.dataInit.objectPool.DisableObject(gameObject);
    }
}