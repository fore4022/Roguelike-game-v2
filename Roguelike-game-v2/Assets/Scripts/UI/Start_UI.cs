public class Start_UI : Button_Default
{
    protected override void PointerClick()
    {
        if(Managers.UserData.data.GetStageState() != StageState.Locked)
        {
            button.interactable = false;
            Managers.Game.DataLoad();
        }
        else
        {
            Managers.UI.HideUI<ToastMessage_UI>();
            Managers.UI.ShowUI<ToastMessage_UI>();
        }
    }
}