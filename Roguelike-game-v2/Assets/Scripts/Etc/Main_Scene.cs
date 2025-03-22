using UnityEngine;
public class Main_Scene : MonoBehaviour
{
    private void Start()
    {
        bool isLevelUp = false;

        while(Managers.UserData.data.Exp < Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1])
        {
            Managers.UserData.data.Exp -= Managers.UserData.UserLevelInfo.requiredEXP[Managers.UserData.data.Level - 1];
            Managers.UserData.data.Level++;

            isLevelUp = true;
        }

        if(isLevelUp)
        {
            Managers.UI.ShowUI<UserLevelUp_UI>();
        }
    }
}