using UnityEngine;
public class StatSelection : MonoBehaviour
{
    private float health = 0;
    private float damage = 0;
    private float moveSpeed = 0;
    private float increaseHealth = 0;
    private float increaseDamage = 0;

    public void Set(float health, float damage, float moveSpeed, float increaseHealth, float increaseDamage)
    {
        this.health = health;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.increaseHealth = increaseHealth;
        this.increaseDamage = increaseDamage;
    }
}