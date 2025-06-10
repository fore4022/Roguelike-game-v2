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
        moveSpeed = (int)stat.defaultStat.moveSpeed;
        increaseHealth = (int)stat.increaseHealth;
        increaseDamage = (int)stat.increaseDamage;
        healthRegenPerSec = (int)stat.healthRegenPerSec;
    }
}