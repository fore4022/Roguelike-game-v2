using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerMove move = new();

    private PlayerInformation information = new();

    private const string statPath = "PlayerInformationSO";

    public DefaultStat Stat { get { return information.stat; } }
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
    private void Update()
    {
        Debug.Log(EventSystem.current.IsPointerOverGameObject());
    }
}