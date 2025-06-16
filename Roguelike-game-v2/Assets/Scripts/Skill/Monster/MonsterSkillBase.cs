using System;
using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class MonsterSkillBase : MonoBehaviour, IDamage
{
    protected SpriteRenderer render;
    protected Animator animator;
    protected AudioSource audioSource;
    protected Rigidbody2D rigid;
    protected Collider2D col;

    protected bool isInit = false;

    private Func<float> damage = null;
    private Plane[] planes = new Plane[6];

    public Func<float> Damage { get { return damage; } set { damage = value; } }
    public float DamageAmount { get { return damage.Invoke(); } }
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
        rigid = GetComponent<Rigidbody2D>();

        rigid.gravityScale = 0;

        if(col == null)
        {
            col = GetComponent<Collider2D>();
        }
    }
    private void OnDisable()
    {
        if(isInit)
        {
            SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnter(collision.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnEnter(collision.gameObject);
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
    private void OnEnter(GameObject go)
    {
        if(go.CompareTag("Player"))
        {
            Enter(go);
        }
    }
    protected abstract void Enter(GameObject go);
    protected abstract void Enable();
}