using System;
using System.Collections;
using UnityEngine;
public class PlayerData
{
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private PlayerInformation info = null;

    private const float baseExperience = 1;//5;

    private int increaseValue;
    private int maxLevel;
    private int levelUpCount = 0;

    public int IncreaseValue { get { return increaseValue; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value * Skill_SO.maxLevel; } }
    public int LevelUpCount { get { return levelUpCount; } }
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
                    info.experience -= info.experienceForLevelUp;
                    levelUpCount++;
                    Level++;

                    SetRequiredExperience();
                }

                experienceUpdate?.Invoke();
            }
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
        info.experienceForLevelUp += 0;//ExperienceForLevelUp * MathF.Max(0.85f - 0.055f * (Level - 1), 0.185f);
    }
    public void SetLevel()
    {
        Level = 1;
    }
    public IEnumerator LevelUp()
    {
        if(levelUpCount == 0)
        {
            Managers.UI.Show<SkillSelection_UI>();
        }
        else
        {
            while(levelUpCount > 0)
            {
                Debug.Log(levelUpCount);
                    
                //yield return new WaitUntil(() => !Managers.UI.Get<SkillSelection_UI>().gameObject.activeSelf);

                levelUpCount--;

                Managers.UI.Get<SkillSelection_UI>().Set();

                yield return new WaitForSecondsRealtime(3);
            }

            Managers.UI.Hide<SkillSelection_UI>();
        }

        Debug.Log("aaa");
    }
}