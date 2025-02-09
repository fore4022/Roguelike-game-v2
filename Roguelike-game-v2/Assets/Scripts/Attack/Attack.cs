using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Attack : MonoBehaviour, IScriptableData, IDamage
{
    protected Attack_SO attackSO;
    protected Animator animator;
    protected SpriteRenderer render;
    protected Collider2D col;

    protected int level;

    private Coroutine attacking = null;
    private string attackType;

    public ScriptableObject SetScriptableObject { set { attackSO = value as Attack_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * attackSO.damageCoefficient[level]; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        StartCoroutine(StartAttack());
    }
    protected void Start()
    {
        Init();
    }
    private void Init()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        render.enabled = false;

        attackType = GetType().ToString();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Monster"))
        {
            return;
        }

        if(collision.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    protected virtual IEnumerator StartAttack()
    {
        yield return new WaitUntil(() => (animator != null) && (render != null) && (col != null));

        level = Managers.Game.inGameData.attackData.GetAttackLevel(attackType);

        SetAttack();

        render.enabled = true;
        col.enabled = true;
        attacking = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attacking == null);

        Managers.Game.inGameData.dataInit.objectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackSO.kinematicDuration);

        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        render.enabled = false;
        attacking = null;
    }
    protected abstract void SetAttack();
}