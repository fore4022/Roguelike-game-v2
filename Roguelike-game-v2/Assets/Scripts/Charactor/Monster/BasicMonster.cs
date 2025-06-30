using System.Collections;
using UnityEngine;
/// <summary>
/// 단순히 플레이어를 향해 이동하는 기본 몬스터이다.
/// </summary>
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    protected const float speedMultiplierDefault = 1;

    protected Vector3 direction;
    protected float speedMultiplier = speedMultiplierDefault;
    protected bool canSwitchDirection = true;

    private const float death_AnimationDuration = 0.3f;
    private const float damagedDuration = 0.15f;

    private Coroutine moveCoroutine = null;
    private WaitForSeconds damaged = new(damagedDuration);
    private Color defaultColor;

    public float SpeedAmount { get { return stat.moveSpeed * speedMultiplier; } }
    public float DamageAmount { get { return stat.damage * Managers.Game.difficultyScaler.IncreaseStat * Time.deltaTime; } }
    protected override void OnEnable()
    {
        base.OnEnable();

        SetPosition();
        changeDirection();

        render.color = defaultColor;
        moveCoroutine = StartCoroutine(Moving());
    }
    public void OnMove()
    {
        rigid.velocity = direction * SpeedAmount;
    }
    protected virtual void SetDirection()
    {
        if(!Managers.Game.IsGameOver)
        {
            if(canSwitchDirection)
            {
                direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);
            }
            else
            {
                return;
            }
        }
    }
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
    }
    protected virtual void Die()
    {
        StopCoroutine(moveCoroutine);

        rigid.simulated = false;

        StartCoroutine(Dieing());
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Attack(collision);
    }
    protected void OnCollisionStay2D(Collision2D collision)
    {
        Attack(collision);
    }
    public void TakeDamage(IDamage damage)
    {
        health -= damage.DamageAmount;

        audioSource.Play();
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
    }
    private void Attack(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Managers.Game.player.TakeDamage(this);
        }
    }
    private IEnumerator Moving()
    {
        animator.Play(0, 0);

        while(true)
        {
            if(isVisible)
            {
                changeDirection();
            }

            SetDirection();
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

        StartCoroutine(ColorLerp.ChangeColor(render, Color.black, defaultColor, death_AnimationDuration / 2));

        yield return new WaitForSeconds(death_AnimationDuration / 2);

        Managers.Game.inGameData.player.Experience += user_Experience;
        Managers.Game.UserExp += inGame_Experience;
        speedMultiplier = speedMultiplierDefault;

        StartCoroutine(ColorLerp.ChangeAlpha(render, 0, render.color.a, death_AnimationDuration));

        yield return new WaitForSeconds(death_AnimationDuration);

        render.color = defaultColor;

        gameObject.SetActive(false);
    }
}