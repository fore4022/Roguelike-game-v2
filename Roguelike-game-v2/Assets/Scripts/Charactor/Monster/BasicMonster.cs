using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// <para>
/// 플레이어를 향해서 이동하는 기본 몬스터
/// </para>
/// IDamage, IDamageReceiver, IMoveable을 구현
/// </summary>
public class BasicMonster : Monster, IDamage, IDamageReceiver, IMoveable
{
    protected const float speedMultiplierDefault = 1;
    protected const float damageMultiplierDefault = 1;
    protected const float directionMultiplierDefault = 1;
    protected const float death_AnimationDuration = 0.3f;

    protected Action onDamaged = null;
    
    protected Color defaultColor;
    protected Vector2 direction = default;
    protected float speedMultiplier = speedMultiplierDefault;
    protected float damageMultiplier = damageMultiplierDefault;
    protected float directionMultiplier = directionMultiplierDefault;
    protected bool canSwitchDirection = true;

    private IMoveable moveable;

    private const float damagedDuration = 0.15f;
    
    private Coroutine moveCoroutine = null;
    private WaitForSeconds damaged = new(damagedDuration);

    public float SpeedAmount { get { return stat.moveSpeed * speedMultiplier * SlowDownAmount; } }
    public float SlowDownAmount { get { return moveable.SlowDownAmount; } }
    public float DamageAmount { get { return stat.damage * damageMultiplier * Managers.Game.difficultyScaler.IncreaseStat * Time.deltaTime; } }
    // IMoveable 구현
    protected override void Awake()
    {
        moveable = new DefaultMoveable();

        base.Awake();
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        Enable();
    }
    // 컴포넌트 설정
    protected override void Set()
    {
        base.Set();

        render.color = defaultColor;
        render.enabled = true;
        rigid.simulated = true;
    }
    // 위치 조정 및 이동 코루틴 실행
    protected virtual void Enable()
    {
        SetPosition();
        Set();
        animator.Play(0, 0);

        moveCoroutine = StartCoroutine(Moving());
    }
    // 이동 속도 감소
    public void SetSlowDown(float slowDown, float duration)
    {
        moveable.SetSlowDown(slowDown, duration);
    }
    // 이동 처리
    public virtual void OnMove()
    {
        rigid.linearVelocity = direction * SpeedAmount;
    }
    // 자기 자신의 위치를 기준으로 플레이어로 향하는 방향 구하기, 배율에 따른 유효 회전 제한
    protected virtual void SetDirection()
    {
        if(!Managers.Game.GameOver)
        {
            if(canSwitchDirection)
            {
                if(direction == default)
                {
                    direction = Default_Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position);
                }
                else
                {
                    if(directionMultiplier == 0)
                    {
                        return;
                    }

                    direction = Vector3.Slerp(direction, Default_Calculate.GetDirection(Managers.Game.player.gameObject.transform.position, transform.position), directionMultiplier).normalized;
                }
            }
        }
    }
    // 피격 효과 재생
    protected virtual void Damaged()
    {
        StartCoroutine(TakingDamage());
    }
    // 충돌 비활성화, 이동 중지
    protected virtual void Die()
    {
        rigid.simulated = false;

        StopCoroutine(moveCoroutine);
    }
    // 충돌 : Collision
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        Enter(collision);
    }
    // 충돌 : Trigger
    protected void OnCollisionStay2D(Collision2D collision)
    {
        Enter(collision);
    }
    // 이벤트 호출, 데미지 로그 출력, 사망 확인
    public void TakeDamage(IDamage damage)
    {
        health -= damage.DamageAmount;

        Managers.Game.damageLog_Manage.Show(transform.position, damage.DamageAmount);
        onDamaged.Invoke();

        if(health <= 0)
        {
            Die();
            StartCoroutine(Dieing());
        }
    }
    // 이벤트 등록 및 초기화
    protected override void Init()
    {
        base.Init();

        onDamaged += audioSource.Play;
        onDamaged += Damaged;

        defaultColor = render.color;
    }
    // 충돌 대상 확인, 플레이어일 경우 공격 수행
    private void Enter(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Attack();
        }
    }
    // 플레이어 공격
    protected virtual void Attack()
    {
        Managers.Game.player.TakeDamage(this);
    }
    // 이동 및 방향 전환 코루틴, 카메라 영역에 보이는 경우 FlipX 실행
    private IEnumerator Moving()
    {
        while(true)
        {
            if(isVisible)
            {
                FlipX();
            }

            SetDirection();
            OnMove();

            yield return null;
        }
    }
    // 사망 효과, 경험치 지급, 오브젝트 풀 반환
    protected virtual IEnumerator Dieing()
    {
        animator.speed = 0;

        StartCoroutine(ColorUtil.ChangeColor(render, Color.black, defaultColor, death_AnimationDuration / 2));

        yield return new WaitForSeconds(death_AnimationDuration / 2);

        Managers.Game.inGameData_Manage.player.Experience += user_Experience;
        Managers.Game.UserExp += inGame_Experience;
        speedMultiplier = speedMultiplierDefault;
        damageMultiplier = damageMultiplierDefault;
        directionMultiplier = directionMultiplierDefault;

        StartCoroutine(ColorUtil.ChangeAlpha(render, 0, render.color.a, death_AnimationDuration));

        yield return new WaitForSeconds(death_AnimationDuration);

        render.color = defaultColor;

        Managers.Game.objectPool.DisableObject(gameObject, monsterSO.name);
    }
    // 피격 효과
    private IEnumerator TakingDamage()
    {
        render.material.SetFloat("_Float", 1);

        yield return damaged;

        render.material.SetFloat("_Float", 0);
    }
}