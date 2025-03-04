using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;

    private PlayerInformation information = new();
    private Animator anime;

    private const string statPath = "PlayerInformation_SO";

    private Vector3 diePosition = new Vector2(0, -0.25f);

    public DefaultStat Stat { get { return information.stat; } }
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
    public IEnumerator Die()
    {
        Coroutine coroutine = StartCoroutine(ObjectManipulator.TrasnformPosition(transform, transform.position - diePosition, 0.5f));

        anime.Play("death");
        
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        Managers.Game.GameEnd();
    }
    public void AnimationPlay(string name)
    {
        anime.Play(name);
    }
    private IEnumerator Init()
    {
        LoadPlayerStat();

        Managers.Game.inGameData.player.Info = information;

        yield return new WaitUntil(() => information.stat != null);

        Managers.Game.player = this;

        move.Init();
    }
    private async void LoadPlayerStat()
    {
        PlayerInforamtion_SO info = await Util.LoadingToPath<PlayerInforamtion_SO>(statPath);

        information.stat = info.stat;
    }
}