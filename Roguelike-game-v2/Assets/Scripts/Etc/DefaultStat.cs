[System.Serializable]
public class DefaultStat
{
    public float health;
    public float damage;
    public float attackSpeed;
    public float moveSpeed;
    public float size;
    public float death_AnimationDuration = 0.5f;

    public DefaultStat(DefaultStat stat)
    {
        health = stat.health;
        damage = stat.damage;
        attackSpeed = stat.attackSpeed;
        moveSpeed = stat.moveSpeed;
        size = stat.size;
        death_AnimationDuration = stat.death_AnimationDuration;
    }
}