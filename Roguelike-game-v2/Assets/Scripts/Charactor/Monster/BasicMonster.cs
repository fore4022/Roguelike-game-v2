using System.Collections;
using UnityEngine;
/// <summary>
/// 단순히 플레이어를 향해 이동하는 기본 몬스터이다.
/// </summary>
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    protected const float speedMultiplierDefault = 1;
    protected const float directionMultiplierDefault = 1;
    protected const float death_AnimationDuration = 0.3f;

    protected Color defaultColor;
    protected Vector2 direction = default;
    protected float speedMultiplier = speedMultiplierDefault;
    protected float directionMultiplier = directionMultiplierDefault;
    protected bool canSwitchDirection = true;

    private IMoveable moveable;

    private const float damagedDuration = 0.15f;
    
    private Coroutine moveCoroutine = null;
    private WaitForSeconds damaged = new(damagedDuration);

    public float SpeedAmount { get { return stat.moveSpeed * speedMultiplier * SlowDownAmount; } }
    public float SlowDownAmount { get { return moveable.SlowDownAmount; } }
    public float DamageAmount { get { return stat.damage * Managers.Game.difficultyScaler.IncreaseStat * Time.deltaTime; } }
    protected override void Awake()
    {
        moveable = Managers.Game.container.Get<DefaultMoveable>(transform);

        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        Enable();
    }
    protected override void Set()
    {
        base.Set();

        moveCoroutine = StartCoroutine(Moving());
        render.color = defaultColor;
        render.enabled = true;
        rigid.simulated = true;
    }
    protected virtual void Enable()
    {
        SetPosition();
        changeDirection();
        Set();
    }
    public void SetSlowDown(float slowDown, float duration)
    {
        moveable.SetSlowDown(slowDown, duration);
    }
    public virtual void OnMove()
    {
        rigid.velocity = direction * SpeedAmount;
    }
    protected virtual void SetDirection()
    {
        if(!Managers.Game.IsGameOver)
        {
            if(canSwitchDirection)
            {
                if(direction == default)
                {
                    direction = Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);
                }
                else
                {
                    if(directionMultiplier == 0)
                    {
                        return;
                    }

                    direction = Vector3.Slerp(direction, Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position), directionMultiplier).normalized;
                }
            }
        }
    }
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
    }
    protected virtual void Die()
    {
        rigid.simulated = false;

        StopCoroutine(moveCoroutine);
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Enter(collision);
    }
    protected void OnCollisionStay2D(Collision2D collision)
    {
        Enter(collision);
    }
    public void TakeDamage(IDamage damage)
    {
        health -= damage.DamageAmount;

        audioSource.Play();
        Damaged();

        if(health <= 0)
        {
            Die();
            StartCoroutine(Dieing());
        }
    }
    protected override void Init()
    {
        base.Init();

        defaultColor = render.color;
    }
    private void Enter(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    protected virtual void Attack()
    {
        Managers.Game.player.TakeDamage(this);
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
    protected virtual IEnumerator Dieing()
    {
        animator.speed = 0;

        StartCoroutine(ColorLerp.ChangeColor(render, Color.black, defaultColor, death_AnimationDuration / 2));

        yield return new WaitForSeconds(death_AnimationDuration / 2);

        Managers.Game.inGameData.player.Experience += user_Experience;
        Managers.Game.UserExp += inGame_Experience;
        speedMultiplier = speedMultiplierDefault;
        directionMultiplier = directionMultiplierDefault;

        StartCoroutine(ColorLerp.ChangeAlpha(render, 0, render.color.a, death_AnimationDuration));

        yield return new WaitForSeconds(death_AnimationDuration);

        render.color = defaultColor;

        Managers.Game.objectPool.DisableObject(gameObject, monsterSO.name);
    }
    private IEnumerator TakingDamage()
    {
        render.material.SetFloat("_Float", 1);

        yield return damaged;

        render.material.SetFloat("_Float", 0);
    }
}