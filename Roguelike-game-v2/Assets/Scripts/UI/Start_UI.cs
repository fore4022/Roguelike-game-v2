public class Start_UI : Button_Default
{
    protected override void PointerClick()
    {
        button.interactable = false;
        Managers.Game.DataLoad();
    }
}