using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    public string basicAttackPrefabName = "S0191";

    private TouchControls touchControls;

    private PlayerBasicAttack basicAttack;
    private PlayerMove move;

    private Stat_SO playerStat;
    private BasicAttack_SO basicAttackType;
    private Stat stat;

    private Coroutine basicAttackStart = null;

    public Stat Stat { get { return stat; } }
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
        stat.health -= damage.Damage;

        if(stat.health < 0) { Die(); }
    }
    private void Update()
    {
        Debug.Log("asdf");
    }
}