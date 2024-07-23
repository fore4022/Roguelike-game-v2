using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    private TouchControls touchControls;

    private PlayerAttack playerAttack;
    private PlayerMove playerMove;

    private Stat_SO playerStat;
    private Stat stat;

    private void Awake()
    {
        touchControls = new();
        playerAttack = new();
        playerMove = new();

        touchControls.Touch.TouchPosition.Enable();
    }
    private void Start()
    {
        touchControls.Touch.TouchPosition.started += ctx => Move();

        //stat = playerStat.stat;
        InvokeRepeating("playerAttack.Attack", 0, stat.attackSpeed);
    }
    private void Update()
    {

    }
    public void Die()
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IDamage damage)
    {
        stat.health -= damage.Damage;

        if(stat.health < 0) { Die(); }
    }
    public void Move()
    {
        //player move
    }
}
