using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;

    private PlayerInformation information = new();
    private Animator anime;

    private const string statPath = "PlayerInformation_SO";
    private const float targetScale = 2.4f;
    private const float duration = 0.4f;

    private Vector3 diePosition = new Vector2(0, 0.15f);
    private bool death = false;

    public DefaultStat Stat { get { return information.stat; } }
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
        information.stat.health -= damage.DamageAmount;

        if(information.stat.health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    public void Reset()
    {
        death = false;

        LoadPlayerStat();
        anime.Play("idle");
    }
    public IEnumerator Die()
    {
        InputActions.DisableInputAction<TouchControls>();

        StartCoroutine(ObjectManipulator.TrasnformPosition(transform, transform.position + diePosition, duration));
        StartCoroutine(ObjectManipulator.SetScale(transform, targetScale, duration));

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
        PlayerInforamtion_SO info = Util.LoadingToPath<PlayerInforamtion_SO>(statPath);

        information.stat = info.stat;
    }
}