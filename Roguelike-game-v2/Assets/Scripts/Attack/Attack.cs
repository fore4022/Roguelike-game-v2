using System.Collections;
using UnityEngine;
public abstract class Attack : RenderableObject, IDamage
{
    protected Attack_SO attackSO;

    protected Animator animator;
    protected Collider2D col;

    private Coroutine attackCoroutine = null;

    public float DamageAmount { get { return Managers.Game.player.Stat.damage * attackSO.damageCoefficient; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    private void Init()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        attackSO = ObjectPool.GetScriptableObject<Attack_SO>(name);
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
        Init();

        render.enabled = false;

        yield return new WaitUntil(() => attackSO != null);

        SetAttack();

        render.enabled = true;
        col.enabled = true;

        attackCoroutine = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attackCoroutine == null);

        render.enabled = false;

        ObjectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= attackSO.kinematicDuration);

        col.enabled = false;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        attackCoroutine = null;
    }
    protected abstract void SetAttack();
}