using UnityEngine;
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class MonsterSkillBase : MonoBehaviour, IDamage
{
    protected IDamage damage;
    protected Collider2D col;
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
        
    }
    private void Init()
    {
        render = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    //private void 
}