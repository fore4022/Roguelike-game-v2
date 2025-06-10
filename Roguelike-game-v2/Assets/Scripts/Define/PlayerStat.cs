[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    public int statPoint = 0;

    private const string sceneName = "Main";
    private const float coef_MoveSpeed = 0.05f;
    private const float coef_IncreaseHealth = 2;
    private const float coef_IncreaseDamage = 2;
    private const float coef_HealthRegenPerSec = 2;

    private int moveSpeed = 0;
    private int increaseHealth = 0;
    private int increaseDamage = 0;
    private int healthRegenPerSec = 0;

    public int MoveSpeed
    {
        get
        {
            if(IsSceneMain())
            {
                return moveSpeed;
            }
            else
            {
                return 1;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                moveSpeed = value;
            }
        }
    }
    public int IncreaseHealth
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseHealth;
            }
            else
            {
                return 1;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseHealth = value;
            }
        }
    }
    public int IncreaseDamage
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseDamage;
            }
            else
            {
                return 1;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseDamage = value;
            }
        }
    }
    public int HealthRegenPerSec
    {
        get
        {
            if(IsSceneMain())
            {
                return healthRegenPerSec;
            }
            else
            {
                return 1;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                healthRegenPerSec = value;
            }
        }
    }
    public PlayerStat()
    {
        defaultStat = new(10, 1, 1);
    }
    public bool IsSceneMain()
    {
        return Managers.Scene.CurrentSceneName == sceneName;
    }
}