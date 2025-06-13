using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public abstract class MonsterSkillBase : MonoBehaviour, IDamage
{
    protected IDamage damage;
    protected SpriteRenderer render;
    protected Animator animator;
    protected AudioSource audioSource;
    protected Collider2D col;

    protected bool isInit = false;

    private Plane[] planes = new Plane[6];

    public IDamage Damage { set { damage = value; } }
    public float DamageAmount { get { return damage.DamageAmount; } }
    protected void Awake()
    {
        gameObject.SetActive(false);
    }
    protected void OnEnable()
    {
        if(!isInit)
        {
            Init();

            isInit = true;

            SetActive(false);
        }

        Enable();
    }
    protected void FixedUpdate()
    {
        IsInvisible();
    }
    protected virtual void SetActive(bool isActive)
    {
        render.enabled = isActive;
        animator.speed = isActive ? 1 : 0;
    }
    protected virtual void Init()
    {
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
    }
    private void IsInvisible()
    {
        planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

        if(GeometryUtility.TestPlanesAABB(planes, col.bounds))
        {
            animator.speed = 1;
        }
        else
        {
            animator.speed = 0;
        }
    }
    protected abstract void Enable();
}