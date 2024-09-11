using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private PlayerStat_SO playerStat;

    private string statPath = "PlayerSO";

    public Define.Stat DefaultStat { get { return playerStat.stat; } }
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
        DefaultStat.health -= damage.Damage;

        if(DefaultStat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Init()
    {
        LoadStat();

        yield return new WaitUntil(() => playerStat != null);

        basicAttack = new();
        move = new();

        move.Init();
        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private async void LoadStat()
    {
        playerStat = await Util.LoadToPath<PlayerStat_SO>(statPath);
    }
}