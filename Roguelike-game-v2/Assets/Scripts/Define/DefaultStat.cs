using System;
[System.Serializable]
public class DefaultStat
{
    [NonSerialized]
    public float maxHealth;
    
    public float health;
    public float damage;
    public float moveSpeed;

    public DefaultStat(DefaultStat stat)
    {
        maxHealth = health = stat.health;
        damage = stat.damage;
        moveSpeed = stat.moveSpeed;
    }
    public DefaultStat(float health, float damage, float moveSpeed)
    {
        maxHealth = this.health = health;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
    }
}