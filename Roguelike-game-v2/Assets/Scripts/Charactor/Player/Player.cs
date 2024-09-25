using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private PlayerInformation information = new();

    private string statPath = "PlayerSO";

    public PlayerInformation Information { get { return information; } }
    public DefaultStat Stat { get { return information.Stat; } }
    private void Start()
    {
        Managers.Game.player = this;
    }
    private void Update()
    {
        Debug.Log(information);
    }
    public void Set()
    {
        StartCoroutine(Init());
    }
    public void GetDamage(IDamage damage)
    {
        information.Stat.health -= damage.DamageAmount;

        if(information.Stat.health <= 0)
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

        yield return new WaitUntil(() => information.Stat != null);

        basicAttack = new();
        move = new();

        move.Init();
        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private async void LoadPlayerStat()
    {
        PlayerStat_SO playerStat = await Util.LoadingToPath<PlayerStat_SO>(statPath);

        information.Stat = playerStat.stat;
    }
}