using UnityEngine;
public class Player : MonoBehaviour, IDamageReceiver
{
    [HideInInspector]
    public string basicAttackTypeName = "basicAttackSO";

    public PlayerBasicAttack basicAttack;
    public PlayerMove move;

    private TouchControls touchControls;

    private Stat_SO playerStat;

    private Coroutine basicAttackStart;

    public Stat_SO Stat { get { return playerStat; } }
    private void Awake()
    {
        touchControls = new();
        basicAttack = new();
        move = new();

        touchControls.Touch.TouchPosition.Enable();
    }
    private void Start()
    {
        Managers.Game.player = this;

        Init();
    }
    private void Init()
    {
        touchControls.Touch.TouchPosition.started += ctx => move.StartMove(ctx);
        touchControls.Touch.TouchPosition.performed += ctx => move.OnMove(ctx);
        touchControls.Touch.TouchPosition.canceled += ctx => move.CancelMove(ctx);

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