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

    protected ISkill skill;
    protected Skill_SO so;
    protected SpriteRenderer render;
    protected Animator animator;
    protected AudioSource audioSource;

    protected Coroutine baseCast;
    protected int level;
    protected bool isInit = false;

    private bool isMaxLevel = false;

    public ScriptableObject SO { set { so = value as Skill_SO; } }
    public float DamageAmount { get { return Managers.Game.player.Stat.damage * so.damageCoefficient[level]; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        StartCoroutine(CastSkill());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void Init()
    {
        if(TryGetComponent(out ISkill skill))
        {
            this.skill = skill;
        }

        if(defaultCollider == null)
        {
            defaultCollider = GetComponent<Collider2D>();
        }

        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        defaultCollider.enabled = enable;
        render.enabled = false;
        animator.speed = 0;
        isInit = true;
    }
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Monster"))
        {
            skill.Enter(go);
        }
    }
    private IEnumerator CastSkill()
    {
        if(!isInit)
        {
            Init();
        }

        yield return new WaitUntil(() => isInit);

        level = Managers.Game.inGameData.skill.GetLevel(so.typePath);

        if(level == Skill_SO.maxLevel - 1 && !isMaxLevel)
        {
            render.color = so.maxLevelColor;
            isMaxLevel = true;
        }

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

        skill.Set();

        defaultCollider.enabled = enable;
        render.enabled = true;
        animator.speed = 1;
        baseCast = StartCoroutine(BaseCasting());

        yield return new WaitUntil(() => baseCast == null);

        Managers.Game.objectPool.DisableObject(gameObject);
    }
    private IEnumerator BaseCasting()
    {
        yield return new WaitUntil(() => skill.Finished);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);

        baseCast = null;
        render.enabled = false;
        defaultCollider.enabled = false;
        animator.speed = 0;
    }
}