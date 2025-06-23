using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMove))]
/// <summary>
/// 게임 플레이 준비 완료 후에 초기화 작업을 진행한다.
/// 플레이어가 움직이는 기능은 PlayerMove.cs로 나누어 구현하였다.
/// </summary>
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;
    public Action maxHealthUpdate = null;
    public Action healthUpdate = null;

    private PlayerInformation information = new();
    private DefaultStat stat = null;
    private SpriteRenderer render;
    private Animator animator;

    private readonly Vector3 diePosition = new Vector3(0, 0.5f);
    private readonly Vector3 dieRotation = new Vector3(0, 0, 370);
    private const float targetScale = 10f;
    private const float duration = 0.4f;

    private Coroutine die = null;
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
        move = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    private void Update()
    {
        Health = Mathf.Min(Health + stat.healthRegenPerSec * Time.deltaTime, MaxHealth);
    }
    public void TakeDamage(IDamage damage)
    {
        Health -= damage.DamageAmount;
        
        if(information.stat.health <= 0 && die == null)
        {
            die = StartCoroutine(Dieing());
        }
    }
    public void Reset()
    {
        transform.localScale = Calculate.GetVector(3);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        death = false;

        LoadPlayerStat();
        StopCoroutine(die);
        maxHealthUpdate?.Invoke();
        healthUpdate?.Invoke();
        animator.Play("idle");

        die = null;
    }
    public void AnimationPlay(string animationName)
    {
        animator.Play(animationName);
    }
    private void LoadPlayerStat()
    {
        stat = information.stat = new(Managers.UserData.data.Stat.defaultStat, true);
    }
    public IEnumerator Dieing()
    {
        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.Hide<HpSlider_UI>();

        transform.SetRotation(new(0, 0, 0))
            .SetScale(targetScale, duration)
            .SetPosition(transform.position + diePosition, duration)
            .SetRotation(dieRotation, duration);

        render.sortingLayerID = SortingLayer.NameToID("Skill_Monster");
        death = true;

        animator.Play("death");

        yield return new WaitForSeconds(duration);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Managers.Game.GameOver();
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