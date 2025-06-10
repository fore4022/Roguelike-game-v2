using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Monster : MonoBehaviour, IScriptableData
{
    protected MonsterStat_SO monsterSO = null;
    protected DefaultStat stat;
    protected Rigidbody2D rigid;
    protected Animator animator;
    protected SpriteRenderer render;
    protected AudioSource audioSource;
    protected Collider2D col;

    protected const float spawnRadius = 5;

    protected float health;
    protected float user_Experience;
    protected int inGame_Experience;
    protected bool isVisible = false;

    private Plane[] planes = new Plane[6];

    private bool didInit = false;

    public ScriptableObject SO { set { monsterSO = value as MonsterStat_SO; } }
    protected virtual void Awake()
    {
        render = GetComponent<SpriteRenderer>();

        if(TryGetComponent(out Collider2D col))
        {
            this.col = col;
        }
        else
        {
            gameObject.AddComponent<CircleCollider2D>();
        }

        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        if(!didInit)
        {
            Init();

            didInit = true;
        }
        else
        {
            Set();
        }

        SetPosition();
    }
    private void Update()
    {
        health += Mathf.Min((health + stat.healthRegenPerSec) * Time.deltaTime, stat.maxHealth);
    }
    protected virtual void FixedUpdate()
    {
        IsInvisible();
    }
    private void IsInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            animator.speed = 1;
            isVisible = true;
        }
        else
        {
            animator.speed = 0;
            isVisible = false;
        }
    }
    protected virtual void Set()
    {
        health = stat.health * Managers.Game.difficultyScaler.IncreaseStat;
        animator.speed = 1;
    }
    protected virtual void Init()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        rigid.simulated = false;
        render.enabled = false;
        audioSource.playOnAwake = false;
        stat = monsterSO.stat;
        user_Experience = monsterSO.user_Experience;
        inGame_Experience = monsterSO.inGame_Experience;
        monsterSO = null;

        Set();
    }
    protected virtual void SetPosition()
    {
        float randomValue = Random.Range(0, 360);
        float cameraWidth = Util.CameraWidth / 2 + spawnRadius;
        float cameraHeight = Util.CameraHeight / 2 + spawnRadius;
        float x = Mathf.Cos(randomValue) * cameraWidth;
        float y = Mathf.Sin(randomValue) * cameraHeight;
        transform.position = new Vector2(x, y) + (Vector2)Managers.Game.player.gameObject.transform.position;
        render.enabled = true;
        rigid.simulated = true;
    }
    protected virtual void changeDirection()
    {
        if(transform.position.x != Managers.Game.player.transform.position.x)
        {
            if(transform.position.x > Managers.Game.player.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            else
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }
    }
}