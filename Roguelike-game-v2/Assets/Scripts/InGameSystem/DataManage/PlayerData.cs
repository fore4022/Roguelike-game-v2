using System;
public class PlayerData
{
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private PlayerInformation info = null;

    private const float baseExperience = 5;
    private const float experienceMultiplier = 0.35f;

    private int increaseValue;

    public int IncreaseValue { get { return increaseValue; } }
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
            increaseValue = value;

            levelUpdate?.Invoke();
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
    private void Set()
    {
        info.experienceForLevelUp = baseExperience;
        info.experience = 0;
        info.level = 0;

        levelUpdate += () => Managers.UI.Show<LevelUp_UI>();
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