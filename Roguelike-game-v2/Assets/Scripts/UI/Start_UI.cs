public class Start_UI : Button_Default
{
    protected override void PointerClick()
    {
        Managers.Game.DataLoad();
    }
}