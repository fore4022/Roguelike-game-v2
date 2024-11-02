using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Attack : MonoBehaviour, IDamage
{
    protected Attack_SO attackSO;

    protected Animator animator;
    protected SpriteRenderer render;
    protected Collider2D col;

    private Coroutine startAttack = null;
    private Coroutine attacking = null;

    private string attackType;

    public float DamageAmount { get { return Managers.Game.player.Stat.damage * attackSO.damageCoefficient; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        if (startAttack == null)
        {
            startAttack = StartCoroutine(StartAttack());
        }
    }
    protected void Start()
    {
        Init();

        attackType = GetType().ToString();

        attackSO = ObjectPool.GetScriptableObject<Attack_SO>(attackType);
    }
    private void Init()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Monster"))
        {
            return;
        }

        if(collision.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitUntil(() => (animator != null) && (render != null) && (col != null));

        render.enabled = false;

        yield return new WaitUntil(() => attackSO != null);

        int level = Managers.Game.attackData.GetAttackLevel(attackType);

        SetAttack(level);

        render.enabled = true;
        col.enabled = true;

        attacking = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attacking == null);

        yield return new WaitForSeconds(5f);

        startAttack = null;

        ObjectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackSO.kinematicDuration);

        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        render.enabled = false;

        attacking = null;
    }
    protected abstract void SetAttack(int level);
}