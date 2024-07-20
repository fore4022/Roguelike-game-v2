using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    private PlayerAttack playerAttack;
    private PlayerMove playerMove;

    private Stat_SO playerStat;
    private Stat stat;

    private void Awake()
    {
        playerAttack = new();
        playerMove = new();
    }
    private void Start()
    {
        stat = playerStat.stat;
    }
    private void Update()
    {
        playerAttack.Attack();
    }
    public void GetDamage(IAttackable attackable)
    {
        stat.health -= attackable.Damage;
    }
}
