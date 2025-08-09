using UnityEngine;
[System.Serializable]
public class PlayerStat
{
    [SerializeField]
    private int moveSpeed = 0;
    [SerializeField]
    private int increaseHealth = 0;
    [SerializeField]
    private int increaseDamage = 0;
    [SerializeField]
    private int healthRegenPerSec = 0;

    public DefaultStat defaultStat;

    private const string sceneName = "Main";
    private const float coef_MoveSpeed = 0.065f;
    private const float coef_IncreaseHealth = 10f;
    private const float coef_IncreaseDamage = 0.65f;
    private const float coef_HealthRegenPerSec = 0.115f;

    public PlayerStat()
    {
        defaultStat = new(50, 1, 2, 0);
    }
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
    public bool IsSceneMain()
    {
        return Managers.Scene.CurrentSceneName == sceneName;
    }
}