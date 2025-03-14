using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Attack : MonoBehaviour, IScriptableData, IDamage, IColliderHandler
{
    [SerializeField]
    protected Collider2D defaultCollider = null;
    [SerializeField]
    protected bool enable;

    protected Attack_SO so;
    protected SpriteRenderer render = null;
    protected Animator anime = null;

    protected Coroutine attack = null;
    protected int level;

    private bool isInit = false;

    public ScriptableObject SO { set { so = value as Attack_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * so.damageCoefficient[level]; } }
    public void SetCollider()
    {
        HandleCollider();
    }
    protected virtual void Awake()
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
        if(defaultCollider == null)
        {
            defaultCollider = GetComponent<Collider2D>();
        }

        render = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();

        defaultCollider.enabled = enable;
        render.enabled = false;
        anime.speed = 0;
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
    protected virtual void HandleCollider()
    {
        defaultCollider.enabled = enable;
        enable = !enable;
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
        defaultCollider.enabled = enable;
        attack = StartCoroutine(Attacking());

        yield return new WaitUntil(() => attack == null);

        anime.speed = 0;
        render.enabled = false;
        defaultCollider.enabled = false;

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
    protected virtual void SetAttack()
    {
        throw new System.NotImplementedException($"{GetType().Name} was not redefined.");
    }
}