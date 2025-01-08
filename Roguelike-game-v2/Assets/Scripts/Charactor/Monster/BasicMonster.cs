using System.Collections;
using UnityEngine;
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
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
    private IEnumerator Dieing()
    {
        float animationDuration = stat.death_AnimationDuration;

        animator.enabled = false;

        StartCoroutine(ColorSetting.ChangeColor(render, Color.black, defaultColor, animationDuration / 2));

        yield return new WaitForSeconds(animationDuration / 2);

        StartCoroutine(ColorSetting.ChangeAlpha(render, 0, render.color.a, animationDuration));

        yield return new WaitForSeconds(animationDuration);

        stat = null;

        Managers.Game.inGameData.dataInit.objectPool.DisableObject(gameObject);
    }
}