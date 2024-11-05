using System;
public class PlayerData
{
    public Action<float> healthUpdate = null;
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private PlayerInformation info = null;

    private const float baseExperience = 5;
    private const float experienceMultiplier = 0.35f;

    private int increaseValue;
    private bool isSet = false;

    public int IncreaseValue { get { return increaseValue; } }
    public PlayerInformation Info
    {
        set
        {
            info = value;

            Set();

            isSet = true;
        }
    }
    public int Level
    {
        get { return info.level; }
        set
        {
            info.level = value;
            increaseValue = value;

            levelUpdate?.Invoke();
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

            experienceUpdate?.Invoke();
        }
    }
    public float ExperienceForLevelUp
    {
        get { return info.experienceForLevelUp; }
    }
    public bool IsSet
    {
        get { return isSet; }
    }
    private void Set()
    {
        info.experienceForLevelUp = baseExperience;
        info.experience = 0;
        info.level = 0;
    }
    private void SetRequiredExperience()
    {
        info.experienceForLevelUp += ExperienceForLevelUp * experienceMultiplier;
    }
    public void SetLevel()
    {
        Level = 1;
    }
}