using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 게임 플레이 준비 완료 후에 초기화 작업을 진행한다.
/// 플레이어가 움직이는 기능은 PlayerMove.cs로 나누어 구현하였다.
/// </summary>
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;
    public Action maxHealthUpdate = null;
    public Action healthUpdate = null;

    private const float duration = 0.4f;

    private Player_Information information = new();
    private DefaultStat stat = null;
    private SpriteRenderer render;
    private Animator animator;

    private bool death = false;

    public DefaultStat Stat { get { return stat; } }
    public float MaxHealth
    {
        get { return stat.maxHealth; }
        set
        {
            stat.maxHealth = value;

            maxHealthUpdate?.Invoke();
        }
    }
    public float Health
    {
        get { return stat.health; }
        set
        {
            stat.health = value;

            healthUpdate?.Invoke();
        }
    }
    public bool Death { get { return death; } }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        move = new(render, new DefaultMoveable());
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private void Update()
    {
        Health = Mathf.Min(Health + (stat.healthRegenPerSec + ((MaxHealth - 50) % 20 / 200)) * Time.deltaTime, MaxHealth);
        move.IsPointerOverUI = EventSystem.current.IsPointerOverGameObject();
    }
    public void TakeDamage(IDamage damage)
    {
        Health -= damage.DamageAmount;
        
        if(information.stat.health <= 0 && !death)
        {
            death = true;

            Die();
        }
    }
    public void Reset()
    {
        transform.localScale = new Vector2(3, 3);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        death = false;

        LoadPlayerStat();
        maxHealthUpdate?.Invoke();
        healthUpdate?.Invoke();
        animator.Play("idle");
    }
    public void AnimationPlay(string animationName)
    {
        animator.Play(animationName);
    }
    private void LoadPlayerStat()
    {
        stat = information.stat = new(Managers.UserData.data.Stat.defaultStat, true);
    }
    private void Die()
    {
        render.sortingLayerID = SortingLayer.NameToID("AboveEffect");

        Managers.Game.endEffect.EffectPlay();
        animator.Play("death");
        render.flipX = false;
        transform.SetRotation(new(0, 0, 0))
            .SetScale(10, duration)
            .SetPosition(transform.position + new Vector3(0, 0.5f), duration)
            .SetRotation(new(0, 0, 370), duration);
    }
    private IEnumerator Init()
    {
        LoadPlayerStat();

        Managers.Game.inGameData.player.Info = information;

        yield return new WaitUntil(() => information.stat != null);

        Managers.Game.player = this;

        move.Init();
    }
}