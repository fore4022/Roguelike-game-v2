using UnityEngine;
public class StatSelection : MonoBehaviour
{
    public const int maxLevel = 5;

    private int moveSpeed = 0;
    private int increaseHealth = 0;
    private int increaseDamage = 0;
    private int healthRegenPerSec = 0;

    public void Set(PlayerStat stat)
    {
        moveSpeed = stat.MoveSpeed;
        increaseHealth = stat.IncreaseHealth;
        increaseDamage = stat.IncreaseDamage;
        healthRegenPerSec = stat.HealthRegenPerSec;
    }
}