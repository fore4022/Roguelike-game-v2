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
}