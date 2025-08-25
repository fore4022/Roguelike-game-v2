using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
/// <summary>
/// <para>
/// 모든 몬스터에 대한 기본 구현이다.
/// </para>
/// 모든 몬스터는 ObjectPool로 생성된다. 또한, 생성되는 과정에서 일부 초기화 작업을 진행한다.
/// ObjectPool에서 처음 사용될 때, 남아있는 초기화 작업을 마무리한다.
/// </summary>
public class Monster : MonoBehaviour, IScriptableData
{
    protected MonsterStat_SO monsterSO = null;
    protected DefaultStat stat;
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected SpriteRenderer render;
    protected AudioSource audioSource;
    protected Collider2D col;

    protected const float spawnRadius = 4f;

    protected float health;
    protected float maxHealth;
    protected float user_Experience;
    protected int inGame_Experience;
    protected bool isVisible = false;

    private const float collectDelay = 20;

    private Plane[] planes = new Plane[6];
    private Coroutine collect = null;
    private WaitForSeconds waitCollect = new(collectDelay);
    private bool didInit = false;

    public ScriptableObject SO { set { monsterSO = value as MonsterStat_SO; } }
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        if(!didInit)
        {
            Init();

            didInit = true;
        }
    }
    private void Update()
    {
        health = Mathf.Min(health + stat.healthRegenPerSec * Time.deltaTime, maxHealth);
    }
    protected virtual void FixedUpdate()
    {
        IsInvisible();
    }
    public float Damage()
    {
        return stat.damage * Managers.Game.difficultyScaler.IncreaseStat;
    }
    protected virtual void Set()
    {
        maxHealth = health = stat.health * Managers.Game.difficultyScaler.IncreaseStat;
        animator.speed = 1;
    }
    protected virtual void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            this.col = gameObject.AddComponent<BoxCollider2D>();
        }

        stat = new(monsterSO.stat);
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        rigid.simulated = false;
        render.enabled = false;
        audioSource.playOnAwake = false;
        user_Experience = monsterSO.user_Experience;
        inGame_Experience = monsterSO.inGame_Experience;
    }
    private void IsInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            animator.speed = 1;
            isVisible = true;

            if(collect != null)
            {
                StopCoroutine(collect);

                collect = null;
            }
        }
        else
        {
            animator.speed = 0;
            isVisible = false;

            if(collect == null)
            {
                collect = StartCoroutine(Collecting());
            }
        }
    }
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);
        float x = Mathf.Cos(randomValue) * (Util.CameraWidth / 2 + spawnRadius);
        float y = Mathf.Sin(randomValue) * (Util.CameraHeight / 2 + spawnRadius);

        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position + Managers.Game.player.move.Direction.normalized * 4;
    }
    protected virtual void changeDirection()
    {
        if(transform.position.x != Managers.Game.player.transform.position.x)
        {
            render.flipX = !(transform.position.x > Managers.Game.player.transform.position.x);
        }
    }
    private IEnumerator Collecting()
    {
        yield return waitCollect;

        collect = null;

        gameObject.SetActive(false);
    }
}