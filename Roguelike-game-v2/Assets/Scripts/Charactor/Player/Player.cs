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

        stat = playerStat.stat;
    }
    private void Update()
    {
        // playerAttack.Attack(); -> InvokeRepeating
    }
    public void Die()
    {
        Managers.Game.GameEnd();
    }
    public void GetDamage(IAttackable attackable)
    {
        stat.health -= attackable.Damage;

        if(stat.health < 0) { Die(); }
    }
    public void Move()
    {
        //player move
    }
}
