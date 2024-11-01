using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Attack : MonoBehaviour, IDamage
{
    protected Attack_SO attackSO;

    protected Animator animator;
    protected SpriteRenderer render;
    protected Collider2D col;

    private AttackCaster caster;

    private Coroutine attackCoroutine = null;

    public float DamageAmount { get { return Managers.Game.player.Stat.damage * attackSO.damageCoefficient; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void Start()
    {
        Init();

        attackSO = ObjectPool.GetScriptableObject<Attack_SO>(name);
    }
    private void Init()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    protected virtual void OnEnable()
    {
        StartCoroutine(StartAttack());
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

        SetAttack();

        render.enabled = true;
        col.enabled = true;

        attackCoroutine = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attackCoroutine == null);

        ObjectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackSO.kinematicDuration);

        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        render.enabled = false;

        attackCoroutine = null;
    }
    protected abstract void SetAttack(int level);//
}