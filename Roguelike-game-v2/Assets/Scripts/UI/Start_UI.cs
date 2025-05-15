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
            //
        }
    }
}