using System.Collections;
using UnityEngine;
public class Main_Scene : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Initalizing());
    }
    private void Update()//
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Managers.UserData.data.Exp += 250;

            StartCoroutine(Initalizing());
        }
    }
    private IEnumerator Initalizing()
    {
        yield return new WaitUntil(() => Managers.UI.IsInitalized);

        int levelUpCount = 0;

        while (Managers.UserData.data.Exp >= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1])
        {
            Managers.UserData.data.Exp -= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
            Managers.UserData.data.Level++;

            levelUpCount++;

            if(Managers.UserData.data.Level == UserLevelInfo_SO.maxLevel)
            {
                break;
            }
        }

        if (levelUpCount != 0)
        {
            Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
            Managers.UI.GetUI<ExpSlider_Main_UI>().UpdateExp();
            Managers.UI.GetUI<Level_Main_UI>().UpdateLevel();
        }
    }
}