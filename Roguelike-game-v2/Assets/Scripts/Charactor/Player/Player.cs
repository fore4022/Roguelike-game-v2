using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move;

    private PlayerInformation information = new();

    private string statPath = "PlayerInformationSO";

    public DefaultStat Stat { get { return information.stat; } }
    private void Start()
    {
        Managers.Game.player = this;
    }
    public void Set()
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

        Managers.Game.playerData.Info = information;

        yield return new WaitUntil(() => information.stat != null);

        move = new();

        move.Init();

        Managers.Game.playerData.Level++;//
    }
    private async void LoadPlayerStat()
    {
        PlayerInforamtion_SO info = await Util.LoadingToPath<PlayerInforamtion_SO>(statPath);

        information.stat = info.stat;
    }
}