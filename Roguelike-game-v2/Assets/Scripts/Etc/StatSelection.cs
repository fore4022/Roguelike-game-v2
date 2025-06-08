using UnityEngine;
public class StatSelection : MonoBehaviour
{
    private float health;
    private float damage;
    private float moveSpeed;
    private float increaseHealth;
    private float increaseDamage;

    public void Set(float health, float damage, float moveSpeed, float increaseHealth, float increaseDamage)
    {
        this.health = health;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
        this.increaseHealth = increaseHealth;
        this.increaseDamage = increaseDamage;
    }
}