using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class Attack : MonoBehaviour, IScriptableData, IDamage
{
    [SerializeField]
    protected Collider2D defaultCollider = null;
    [SerializeField]
    protected bool enable;

    protected IAttacker attacker;
    protected Attack_SO so;
    protected SpriteRenderer render = null;
    protected Animator anime = null;

    protected Coroutine baseAttack = null;
    protected int level;

    private bool isInit = false;

    public ScriptableObject SO { set { so = value as Attack_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * so.damageCoefficient[level]; } }
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
        if(TryGetComponent(out IAttacker attacker))
        {
            this.attacker = attacker;
        }

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

        anime.Update(0);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attacker.Enter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        attacker.Enter(collision.gameObject);
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

        attacker.SetAttack();

        anime.speed = 1;
        render.enabled = true;
        defaultCollider.enabled = enable;
        baseAttack = StartCoroutine(BaseAttacking());

        yield return new WaitUntil(() => baseAttack == null);

        anime.speed = 0;
        render.enabled = false;
        defaultCollider.enabled = false;

        Managers.Game.inGameData.init.objectPool.DisableObject(gameObject);
    }
    private IEnumerator BaseAttacking()
    {
        yield return new WaitUntil(() => attacker.Finished);

        yield return new WaitUntil(() => anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        baseAttack = null;
    }
}