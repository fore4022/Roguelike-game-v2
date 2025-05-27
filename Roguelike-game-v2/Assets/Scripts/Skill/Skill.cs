using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Skill : MonoBehaviour, IScriptableData, IDamage
{
    [SerializeField]
    protected Collider2D defaultCollider = null;
    [SerializeField]
    protected bool enable;

    protected ISkill attacker;
    protected Skill_SO so;
    protected SpriteRenderer render;
    protected Animator anime;
    protected AudioSource audioSource;

    protected Coroutine baseAttack;
    protected int level;
    protected bool isInit = false;

    public ScriptableObject SO { set { so = value as Skill_SO; } }
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
        if(TryGetComponent(out ISkill attacker))
        {
            this.attacker = attacker;
        }

        if(defaultCollider == null)
        {
            defaultCollider = GetComponent<Collider2D>();
        }

        render = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        defaultCollider.enabled = enable;
        render.enabled = false;
        anime.speed = 0;
        isInit = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            attacker.Enter(go);
        }
    }
    private IEnumerator StartAttack()
    {
        yield return new WaitUntil(() => isInit);

        level = Managers.Game.inGameData.attack.GetLevel(so.typePath);

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