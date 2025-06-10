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
        moveSpeed = (int)stat.MoveSpeed;
        increaseHealth = (int)stat.IncreaseHealth;
        increaseDamage = (int)stat.IncreaseDamage;
        healthRegenPerSec = (int)stat.HealthRegenPerSec;
    }
    public void Save()
    {
        Managers.UserData.data.Stat.MoveSpeed = moveSpeed;
        Managers.UserData.data.Stat.IncreaseHealth = increaseHealth;
        Managers.UserData.data.Stat.IncreaseDamage = increaseDamage;
        Managers.UserData.data.Stat.HealthRegenPerSec = healthRegenPerSec;

        Managers.UserData.Save();
    }
}