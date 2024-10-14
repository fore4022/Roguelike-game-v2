using System;
using System.Collections.Generic;
public class PlayerDataManage
{
    public Action<(int, int)> levelUpdate = null;
    public Action<float> healthUpdate = null;
    public Action<float> experienceUpdate = null;

    private PlayerInformation info = null;

    private Dictionary<string, (int index, int level)> acquiredAttackTypes = new();

    private const float baseExperience = 5;
    private const float experienceMultiplier = 0.35f;

    public PlayerInformation Info
    {
        set
        {
            info = value;

            Set();
        }
    }
    public int Level
    {
        get { return info.level; }
        set
        {
            info.level = value;

            levelUpdate.Invoke((Level, value));
        }
    }
    public float Health
    {
        get { return info.stat.health; }
        set
        {
            info.stat.health = value;

            healthUpdate?.Invoke(Health);
        }
    }
    public float Experience
    {
        get { return info.experience; }
        set
        {
            info.experience = value;

            if(Experience >= info.experienceForLevelUp)
            {
                int count = 0;

                while (Experience >= info.experienceForLevelUp)
                {
                    info.experience -= info.experienceForLevelUp;

                    count++;
                }

                for (int i = 0; i < count; i++)
                {
                    SetRequiredExperience();
                }

                Level += count;
            }

            experienceUpdate?.Invoke(Experience);
        }
    }
    public void UpgradeOrAddAttackType(string attackName)
    {
        if (acquiredAttackTypes.TryGetValue(attackName, out (int, int) data))
        {
            data.Item2++;
        }
        else
        {
            acquiredAttackTypes.Add(attackName, (acquiredAttackTypes.Count + 1 , 1));
        }
    }
    private void Set()
    {
        info.experienceForLevelUp = baseExperience;
        info.experience = 0;
        info.level = 0;
    }
    private void SetRequiredExperience()
    {
        info.experienceForLevelUp += Experience * experienceMultiplier;
    }
}