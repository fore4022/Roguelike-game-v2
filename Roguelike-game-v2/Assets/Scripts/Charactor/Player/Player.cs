using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    private TouchControls touchControls;

    private PlayerBasicAttack basicAttack;
    private PlayerMove move;

    private Stat_SO playerStat;
    private Stat stat;

    public Stat Stat { get { return stat; } }
    private void Awake()
    {
        touchControls = new();
        basicAttack = new();

        touchControls.Touch.TouchPosition.Enable();
    }
    private void Start()
    {
        touchControls.Touch.TouchPosition.started += ctx => Move();

        stat = playerStat.stat;
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
}
