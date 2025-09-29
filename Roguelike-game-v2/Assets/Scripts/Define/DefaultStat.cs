using System;
/// <summary>
/// 플레이어와 몬스터가 가지는 스탯입니다.
/// </summary>
[Serializable]
public class DefaultStat
{
    [NonSerialized]
    public float maxHealth;

    public float health;
    public float damage;
    public float moveSpeed;
    public float healthRegenPerSec;

    public DefaultStat(float health, float damage, float moveSpeed, float healthRegenPerSec)
    {
        maxHealth = this.health = health;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.healthRegenPerSec = healthRegenPerSec;
    }
    public DefaultStat(DefaultStat stat, bool isPlayer = false)
    {
        if(isPlayer)
        {
            maxHealth = health = stat.health + Managers.UserData.data.Stat.IncreaseHealth;
            damage = stat.damage + Managers.UserData.data.Stat.IncreaseDamage;
            moveSpeed = stat.moveSpeed + Managers.UserData.data.Stat.MoveSpeed;
            healthRegenPerSec = stat.healthRegenPerSec + Managers.UserData.data.Stat.HealthRegenPerSec;
        }
        else
        {
            maxHealth = health = stat.health;
            damage = stat.damage;
            moveSpeed = stat.moveSpeed;
            healthRegenPerSec = stat.healthRegenPerSec;
        }
    }
}