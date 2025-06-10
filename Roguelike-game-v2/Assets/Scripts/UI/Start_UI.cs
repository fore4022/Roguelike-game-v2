public class Start_UI : Button_Default
{
    private const string log = "You must clear the previous stage.";

    protected override void PointerClick()
    {
        if(Managers.UserData.data.GetStageState() != StageState.Locked)
        {
            button.interactable = false;
            Managers.Game.DataLoad();
        }
        else
        {
            Managers.UI.Hide<ToastMessage_UI>();
            Managers.UI.ShowAndGet<ToastMessage_UI>().SetText(log);
        }
    }
}