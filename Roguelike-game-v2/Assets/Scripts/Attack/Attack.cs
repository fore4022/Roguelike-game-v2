using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Attack : MonoBehaviour, IScriptableData, IDamage
{
    protected Attack_SO so;
    protected Animator anime;
    protected SpriteRenderer render;
    protected Collider2D col;

    protected Coroutine attack = null;
    protected int level;

    private bool isInit = false;

    public ScriptableObject SO { set { so = value as Attack_SO; } }
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

        anime.speed = 0;
        render.enabled = false;
        col.enabled = false;
        isInit = true;
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
    private IEnumerator StartAttack()
    {
        yield return new WaitUntil(() => isInit);

        level = Managers.Game.inGameData.attack.GetLevel(so.attackTypePath);

        if(so.flipX)
        {
            if(Random.Range(0, 2) == 1)
            {
                render.flipX = true;
            }
            else
            {
                render.flipX = false;
            }
        }

        if(so.flipY)
        {
            if(Random.Range(0, 2) == 1)
            {
                render.flipY = true;
            }
            else
            {
                render.flipY = false;
            }
        }

        SetAttack();

        anime.speed = 1;
        render.enabled = true;
        col.enabled = true;
        attack = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attack == null);

        anime.speed = 0;
        render.enabled = false;
        col.enabled = false;

        Managers.Game.inGameData.init.objectPool.DisableObject(gameObject);
    }
    protected virtual IEnumerator Attacking()
    {
        if(so.duration == 0)
        {
            yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        }

        attack = null;
    }
    protected abstract void SetAttack();
}