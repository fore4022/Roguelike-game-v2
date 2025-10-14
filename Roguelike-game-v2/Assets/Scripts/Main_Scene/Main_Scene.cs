using System.Collections;
using UnityEngine;
/// <summary>
/// 메인 Scene 초기화
/// </summary>
public class Main_Scene : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
    }
    private void Start()
    {
        StartCoroutine(Initalizing());
    }
    private IEnumerator Initalizing()
    {
        int levelUpCount = 0;

        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        while(Managers.Data.data.Exp >= Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.data.Level - 1])
        {
            Managers.Data.data.Exp -= Managers.Data.UserLevelInfo.requiredEXP[Managers.Data.data.Level - 1];
            Managers.Data.data.Level++;

            levelUpCount++;

            if(Managers.Data.data.Level == UserLevelData_SO.maxLevel)
            {
                break;
            }
        }

        if(levelUpCount != 0)
        {
            Managers.Data.data.StatPoint += levelUpCount;

            Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
            Managers.UI.Get<UserExpSlider_UI>().UpdateExp();
            Managers.UI.Get<UserLevelText_UI>().LevelUpdate();
            Managers.Data.Save();
        }
    }
}