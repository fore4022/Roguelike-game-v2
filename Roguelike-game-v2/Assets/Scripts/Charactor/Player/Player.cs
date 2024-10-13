using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public AttackCaster basicAttack;
    public PlayerMove move;

    private PlayerInformation information = new();

    private string statPath = "PlayerSO";//

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
    private IEnumerator Init()//
    {
        LoadPlayerStat();

        Managers.Game.playerData.Info = information;

        yield return new WaitUntil(() => information.stat != null);

        basicAttack = new();
        move = new();

        move.Init();
        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private async void LoadPlayerStat()//
    {
        PlayerStat_SO playerStat = await Util.LoadingToPath<PlayerStat_SO>(statPath);

        information.stat = playerStat.stat;
    }
}