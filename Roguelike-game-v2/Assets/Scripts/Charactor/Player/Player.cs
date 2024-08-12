using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    public string basicAttackTypeName = "basicAttack";

    private TouchControls touchControls;

    private Stat_SO playerStat;
    private BasicAttack_SO basicAttackType;

    private Coroutine basicAttackStart = null;

    public Stat_SO Stat { get { return playerStat; } }
    private void Awake()
    {
        touchControls = new();
        basicAttack = new();
        basicAttack = new();
        move = new();

        touchControls.Touch.TouchPosition.Enable();
    }
    private void Start()
    {
        ObjectPool.CreateToPath("basicAttack", 20);

        Managers.Game.player = this;

        Init();
    }
    private void Init()
    {
        //stat = playerStat.stat;

        touchControls.Touch.TouchPosition.started += ctx => move.Move();

        basicAttackStart = StartCoroutine(basicAttack.basicAttacking());
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