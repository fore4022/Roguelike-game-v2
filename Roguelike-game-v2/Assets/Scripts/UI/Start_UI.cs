public class Start_UI : Button_Default
{
    protected override void PointerClick()
    {
        if(!Managers.Scene.isSceneLoading)
        {
            Managers.Game.DataLoad();
        }
    }
}