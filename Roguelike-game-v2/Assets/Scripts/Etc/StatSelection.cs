using UnityEngine;
public class StatSelection : MonoBehaviour
{
    private float health = 0;
    private float damage = 0;
    private float moveSpeed = 0;
    private float increaseHealth = 0;
    private float increaseDamage = 0;
    private float healthRegenPerSec = 0;

    public void Set(PlayerStat stat)
    {
        health = stat.defaultStat.health;
        damage = stat.defaultStat.damage;
        moveSpeed = stat.defaultStat.moveSpeed;
        increaseHealth = stat.increaseHealth;
        increaseDamage = stat.increaseDamage;
        healthRegenPerSec = stat.healthRegenPerSec;
    }
}