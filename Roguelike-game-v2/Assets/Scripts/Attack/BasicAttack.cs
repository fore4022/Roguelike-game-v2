using System.Collections;
using UnityEngine;
public abstract class BasicAttack : RenderableObject, IDamage
{
    protected Attack_SO basicAttackSO;

    protected Animator animator;
    protected Coroutine attackCoroutine;

    public float DamageAmount { get { return Managers.Game.player.PlayerStat.damage * basicAttackSO.damageCoefficient; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        attackCoroutine = StartCoroutine(Attacking());
    }
    protected void OnDisable()
    {
        if(attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.GetDamage(this);
        }
    }
    private IEnumerator Attacking()
    {
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();

        render.enabled = false;

        basicAttackSO = ObjectPool.GetScriptableObject<Attack_SO>(name);

        yield return new WaitUntil(() => basicAttackSO != null);

        Set();

        render.enabled = true;

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        render.enabled = false;

        basicAttackSO = null;
        animator = null;
        render = null;
        attackCoroutine = null;

        ObjectPool.DisableObject(gameObject);
    }
    protected abstract void Set();
}