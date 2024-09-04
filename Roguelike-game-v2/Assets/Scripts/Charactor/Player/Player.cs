using System;
using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private Stat_SO playerStat;

    public Stat_SO Stat { get { return playerStat; } }
    private void Awake()
    {
        basicAttack = new();
        move = new();
    }
    private void Start()
    {
        Managers.Game.player = this;

        Init();

        basicAttack.basicAttackCoroutine = StartCoroutine(basicAttack.basicAttacking());
    }
    private void Init()
    {
        move.Init();
    }
    public void Die()//
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IDamage damage)//
    {
        Stat.health -= damage.Damage;

        if(Stat.health < 0) { Die(); }
    }
}