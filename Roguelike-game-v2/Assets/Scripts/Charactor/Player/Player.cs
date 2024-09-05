using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private Stat_SO playerStat;

    private string statPath = "playerStat";

    public Stat_SO Stat { get { return playerStat; } }
    private void Awake()
    {
        basicAttack = new();
        move = new();
    }
    private void Start()
    {
        Managers.Game.player = this;

        StartCoroutine(Init());
    }
    public void Die()
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IDamage damage)
    {
        Stat.health -= damage.Damage;

        if(Stat.health <= 0)
        {
            Die();
        }
    }
    private IEnumerator Init()
    {
        LoadStat();

        yield return new WaitUntil(() => playerStat != null);

        move.Init();
        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private async void LoadStat()
    {
        playerStat = await Util.LoadToPath<Stat_SO>(statPath);
    }
}