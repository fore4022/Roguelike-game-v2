using System;
[System.Serializable]
public class DefaultStat
{
    [NonSerialized]
    public float maxHealth;
    
    public float health;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float size;

    public DefaultStat(DefaultStat stat)
    {
        maxHealth = health = stat.health;
        damage = stat.damage;
        attackSpeed = stat.attackSpeed;
        moveSpeed = stat.moveSpeed;
        size = stat.size;
    }
}