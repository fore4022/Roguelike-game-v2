using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    private TouchControls touchControls;

    private PlayerBasicAttack basicAttack;
    private PlayerMove move;

    private Stat_SO playerStat;
    private BasicAttack_SO basicAttackType;
    private Stat stat;

    public Stat Stat { get { return stat; } }
    private void Awake()
    {
        touchControls.Touch.TouchPosition.Enable();
    }
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        stat = playerStat.stat;

        touchControls = new();
        basicAttack = new();

        touchControls.Touch.TouchPosition.started += ctx => move.Move();
    }
    public void Die()//
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IDamage damage)//
    {
        stat.health -= damage.Damage;

        if(stat.health < 0) { Die(); }
    }
}