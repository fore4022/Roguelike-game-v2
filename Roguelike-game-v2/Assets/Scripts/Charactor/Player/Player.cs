using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMove))]
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = null;

    private PlayerInformation information = new();

    private const string statPath = "PlayerInformationSO";

    public DefaultStat Stat { get { return information.stat; } }
    private void Awake()
    {
        move = GetComponent<PlayerMove>();
    }
    private void Start()
    {
        StartCoroutine(Init());
    }
    public void GetDamage(IDamage damage)
    {
        information.stat.health -= damage.DamageAmount;

        if(information.stat.health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Managers.Game.GameEnd();
    }
    private IEnumerator Init()
    {
        LoadPlayerStat();

        Managers.Game.inGameData.playerData.Info = information;

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