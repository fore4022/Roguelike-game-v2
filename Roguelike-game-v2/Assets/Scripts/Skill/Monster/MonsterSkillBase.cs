using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public abstract class MonsterSkillBase : MonoBehaviour, IDamage
{
    protected IDamage damage;
    protected SpriteRenderer render;
    protected Animator anime;
    protected AudioSource audioSource;

    protected bool isInit = false;

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
    protected virtual void SetActive(bool isActive)
    {
        render.enabled = isActive;
        anime.speed = isActive ? 1 : 0;
    }
    protected virtual void Init()
    {
        render = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    protected abstract void Enable();
}