using System;
using System.Collections;
using UnityEngine;
public class PlayerData
{
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private PlayerInformation info = null;

    private const float baseExperience = 9999999;//5

    private Coroutine levelCalculation = null;
    private int increaseValue;
    private int maxLevel;
    private int levelUpCount = 1;

    public int IncreaseValue { get { return increaseValue; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value * Skill_SO.maxLevel; } }
    public int LevelUpCount { get { return levelUpCount; } set { levelUpCount = value; } }
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
                while(Experience >= info.experienceForLevelUp)
                {
                    Level++;
                    info.experience -= info.experienceForLevelUp;
                    
                    if(Level <= maxLevel)
                    {
                        levelUpCount++;
                    }
                 
                    SetRequiredExperience();
                }

                if(levelCalculation != null)
                {
                    Util.GetMonoBehaviour().StopCoroutine(levelCalculation);
                }

                levelCalculation = Util.GetMonoBehaviour().StartCoroutine(WaitLevelCalculation());
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
    }
    private void SetRequiredExperience()
    {
        info.experienceForLevelUp += ExperienceForLevelUp * MathF.Max(0.85f - 0.055f * (Level - 1), 0.185f);
    }
    public void SetLevel()
    {
        Level = 1;

        Managers.UI.Get<LevelText_InGame_UI>().LevelUpdate();
    }
    private IEnumerator WaitLevelCalculation()
    {
        for(int i = 0; i < 2; i++)
        {
            yield return null;
        }

        levelUpdate.Invoke();

        levelCalculation = null;
    }
}