public class Start_UI : NewButton
{
    protected override void PointerClick()
    {
        Managers.Game.DataLoad();
    }
}