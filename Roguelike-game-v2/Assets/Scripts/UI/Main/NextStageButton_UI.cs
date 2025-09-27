using UnityEngine;
public class NextStageButton_UI : Button_Default
{
    [SerializeField]
    private int sign;

    protected override void PointerClick()
    {
        Managers.UI.Get<StageIcon_UI>().UpdateUI(sign);

        if(Managers.UserData.data.GetStageState() == StageState.Locked)
        {
            //Managers.UI.Hide<StageInformationButton_UI>();
        }
        else
        {
            //if(!Managers.UI.Get<StageInformationButton_UI>().gameObject.activeSelf)
            //{
            //    Managers.UI.Show<StageInformationButton_UI>();
            //}

            //Managers.UI.Get<StageInformationButton_UI>().InformationUpdate();
        }
    }
}