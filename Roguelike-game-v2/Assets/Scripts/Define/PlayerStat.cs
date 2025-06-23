using UnityEngine;
[System.Serializable]
public class PlayerStat
{
    public DefaultStat defaultStat;

    private const string sceneName = "Main";
    private const float coef_MoveSpeed = 0.05f;
    private const float coef_IncreaseHealth = 1.5f;
    private const float coef_IncreaseDamage = 0.6f;
    private const float coef_HealthRegenPerSec = 0.1f;

    [SerializeField]
    private int moveSpeed = 0;
    [SerializeField]
    private int increaseHealth = 0;
    [SerializeField]
    private int increaseDamage = 0;
    [SerializeField]
    private int healthRegenPerSec = 0;

    public float MoveSpeed
    {
        get
        {
            if(IsSceneMain())
            {
                return moveSpeed;
            }
            else
            {
                return moveSpeed * coef_MoveSpeed;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                moveSpeed = (int)value;
            }
        }
    }
    public float IncreaseHealth
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseHealth;
            }
            else
            {
                return increaseHealth * coef_IncreaseHealth;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseHealth = (int)value;
            }
        }
    }
    public float IncreaseDamage
    {
        get
        {
            if(IsSceneMain())
            {
                return increaseDamage;
            }
            else
            {
                return increaseDamage * coef_IncreaseDamage;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                increaseDamage = (int)value;
            }
        }
    }
    public float HealthRegenPerSec
    {
        get
        {
            if(IsSceneMain())
            {
                return healthRegenPerSec;
            }
            else
            {
                return healthRegenPerSec * coef_HealthRegenPerSec;
            }
        }
        set
        {
            if(IsSceneMain())
            {
                healthRegenPerSec = (int)value;
            }
        }
    }
    public PlayerStat()
    {
        defaultStat = new(100, 1, 1, 0);
    }
    public bool IsSceneMain()
    {
        return Managers.Scene.CurrentSceneName == sceneName;
    }
}