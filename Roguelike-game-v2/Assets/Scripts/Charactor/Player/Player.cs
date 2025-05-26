using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;
    public Action maxHealthUpdate = null;
    public Action healthUpdate = null;

    private PlayerInformation information = new();
    private PlayerStat_SO playerStatSO = null;
    private DefaultStat stat = null;

    private Animator anime;

    private const string statPath = "PlayerOriginalStat_SO";
    private const float targetScale = 3.5f;
    private const float duration = 0.4f;

    private Coroutine die = null;
    private Vector3 diePosition = new Vector2(0, 0.3f);
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
        anime = GetComponent<Animator>();
    }
    private void Start()
    {
        StartCoroutine(Init());
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
        death = false;

        LoadPlayerStat();
        StopCoroutine(die);
        anime.Play("idle");

        die = null;
    }
    public IEnumerator Dieing()
    {
        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.HideUI<HpSlider_UI>();

        transform.SetPosition(transform.position + diePosition, duration).SetScale(targetScale, duration);

        death = true;

        anime.Play("death");

        yield return new WaitForSeconds(duration);

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Managers.Game.GameOver();
    }
    public void AnimationPlay(string animationName)
    {
        anime.Play(animationName);
    }
    private IEnumerator Init()
    {
        LoadPlayerStat();

        Managers.Game.inGameData.player.Info = information;

        yield return new WaitUntil(() => information.stat != null);

        Managers.Game.player = this;

        move.Init();
    }
    private void LoadPlayerStat()
    {
        if(playerStatSO == null)
        {
            playerStatSO = Util.LoadingToPath<PlayerStat_SO>(statPath);
        }

        stat = information.stat = new(playerStatSO.stat);
    }
}