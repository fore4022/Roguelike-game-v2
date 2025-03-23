using UnityEngine;
public class Main_Scene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int levelUpCount = 0;

            Managers.UserData.data.Exp += 250;

            while(Managers.UserData.data.Exp >= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1])
            {
                Managers.UserData.data.Exp -= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
                Managers.UserData.data.Level++;

                levelUpCount++;
            }

            if(levelUpCount != 0)
            {
                Managers.UI.ShowAndGet<UserLevelUp_UI>().PlayEffect(levelUpCount);
                Managers.UI.GetUI<ExpSlider_Main_UI>().UpdateExp();
                Managers.UI.GetUI<Level_Main_UI>().UpdateLevel();
            }
        }
    }
}