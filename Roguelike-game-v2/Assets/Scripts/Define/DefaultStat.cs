using System;
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
    public DefaultStat(DefaultStat stat)
    {
        maxHealth = health = stat.health + Managers.UserData.data.Stat.IncreaseHealth;
        damage = stat.damage + Managers.UserData.data.Stat.IncreaseDamage;
        moveSpeed = stat.moveSpeed + Managers.UserData.data.Stat.MoveSpeed;
        healthRegenPerSec = stat.healthRegenPerSec + Managers.UserData.data.Stat.HealthRegenPerSec;
    }
}