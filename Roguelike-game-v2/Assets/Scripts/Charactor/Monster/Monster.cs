using UnityEngine;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Monster : RenderableObject, IScriptableData
{
    protected MonsterStat_SO monsterSO = null;
    protected DefaultStat stat;
    protected Rigidbody2D rigid;
    protected Animator animator;

    protected const float spawnRadius = 5;

    protected float experience;
    protected float health;

    private bool didInit = false;

    public ScriptableObject SetScriptableObject { set { monsterSO = value as MonsterStat_SO; } }
    protected override void Awake()
    {
        base.Awake();

        gameObject.SetActive(false);
    }
    protected virtual void OnEnable()
    {
        if(!didInit)
        {
            Init();
            LoadStat();
        }
        else
        {
            health = monsterSO.stat.health;
        }

        SetPosition();
    }
    protected virtual void LoadStat()
    {
        stat = monsterSO.stat;
        experience = monsterSO.experience;
        health = stat.health;
    }
    protected virtual void Init()
    {
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        render.enabled = false;
        rigid.simulated = false;
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