using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// 플레이어의 레벨, 경험치, 정보를 관리하는 역할
/// </summary>
public class PlayerData
{
    public Action experienceUpdate = null;
    public Action levelUpdate = null;

    private Player_Information info = null;

    private const float baseExperience = 5;

    private Coroutine levelCalculation = null;
    private int increaseValue;
    private int maxLevel;
    private int levelUpCount = 1;

    public Player_Information Info { set { info = value; } }
    public int IncreaseValue { get { return increaseValue; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value * Skill_SO.maxLevel; } }
    public int LevelUpCount { get { return levelUpCount; } set { levelUpCount = value; } }
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
                    CoroutineHelper.Stop(levelCalculation);
                }

                levelCalculation = CoroutineHelper.Start(WaitLevelCalculation(), CoroutineType.Manage);
            }

            experienceUpdate?.Invoke();
        }
    }
    public float ExperienceForLevelUp { get { return info.experienceForLevelUp; } }
    private void SetRequiredExperience()
    {
        info.experienceForLevelUp += ExperienceForLevelUp * MathF.Max(0.75f - 0.195f * (Level - 1), 0.115f);
    }
    public void SetLevel()
    {
        info.experienceForLevelUp = baseExperience;
        info.experience = 0;
        Level = 1;

        Managers.UI.Get<LevelText_UI>().LevelUpdate();
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