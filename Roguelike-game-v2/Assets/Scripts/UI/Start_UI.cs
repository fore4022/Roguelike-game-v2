public class Start_UI : NewButton
{
    public override void PointerClick()
    {
        Managers.Game.DataLoad();
    }
}