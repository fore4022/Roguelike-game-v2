using UnityEngine;
/// <summary>
/// 플레이어 스탯 관리
/// </summary>
public class Stat_Manage : MonoBehaviour
{
    public const int maxLevel = 5;

    private float moveSpeed = 0;
    private float increaseHealth = 0;
    private float increaseDamage = 0;
    private float healthRegenPerSec = 0;

    public void Set(PlayerStat stat)
    {
        moveSpeed = stat.MoveSpeed;
        increaseHealth = stat.IncreaseHealth;
        increaseDamage = stat.IncreaseDamage;
        healthRegenPerSec = stat.HealthRegenPerSec;
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