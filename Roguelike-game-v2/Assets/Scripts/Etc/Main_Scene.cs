using System.Collections;
using UnityEngine;
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

        while(Managers.UserData.data.Exp >= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1])
        {
            Managers.UserData.data.Exp -= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
            Managers.UserData.data.Level++;

            levelUpCount++;

            if(Managers.UserData.data.Level == UserLevelInfo_SO.maxLevel)
            {
                break;
            }
        }

        if(levelUpCount != 0)
        {
            Managers.UserData.data.Stat.statPoint++;

            Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
            Managers.UI.Get<ExpSlider_Main_UI>().UpdateExp();
            Managers.UI.Get<Level_Main_UI>().UpdateLevel();
        }
    }
}