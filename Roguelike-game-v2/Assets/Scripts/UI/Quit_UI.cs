public class Quit_UI : Button_A
{
    protected override void PointerClick()
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneName.Main, false);
    }
}