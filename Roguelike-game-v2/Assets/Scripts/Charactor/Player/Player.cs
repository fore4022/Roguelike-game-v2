using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private DefaultStat stat;

    private string statPath = "PlayerSO";

    public DefaultStat Stat { get { return stat; } }
    private void Start()
    {
        Managers.Game.player = this;
    }
    public void Set()
    {
        StartCoroutine(Init());
    }
    public void Die()
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IDamage damage)
    {
        stat.health -= damage.DamageAmount;

        if(stat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Init()
    {
        LoadStat();

        yield return new WaitUntil(() => stat != null);//player information

        basicAttack = new();
        move = new();

        move.Init();
        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private async void LoadStat()
    {
        PlayerStat_SO playerStat = await Util.LoadingToPath<PlayerStat_SO>(statPath);

        stat = playerStat.stat;
    }
}