using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Attack : MonoBehaviour, IScriptableData, IDamage
{
    protected Attack_SO so;
    protected Animator anime;
    protected SpriteRenderer render;
    protected Collider2D col;

    protected int level;

    private Coroutine isAttacking = null;
    private string attackType;

    public ScriptableObject SetSO { set { so = value as Attack_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * so.damageCoefficient[level]; } }
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
        anime = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        render.enabled = false;

        attackType = GetType().ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enter(collision.gameObject);
    }
    protected virtual void Enter(GameObject go)
    {
        if(!go.CompareTag("Monster"))
        {
            return;
        }

        if(go.TryGetComponent(out IDamageReceiver damageReceiver))
        {
            damageReceiver.TakeDamage(this);
        }
    }
    protected virtual IEnumerator StartAttack()
    {
        yield return new WaitUntil(() => attackType != null);

        level = Managers.Game.inGameData.attack.GetLevel(attackType);

        SetAttack();

        render.enabled = true;
        col.enabled = true;
        isAttacking = StartCoroutine(Attacking());

        yield return new WaitUntil(() => isAttacking == null);

        Managers.Game.inGameData.dataInit.objectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= so.kinematicDuration);

        col.enabled = false;

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        render.enabled = false;
        isAttacking = null;
    }
    protected abstract void SetAttack();
}