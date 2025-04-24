public class Quit_UI : Button_1
{
    protected override void PointerClick()
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(Define.SceneName.Main, false);
    }
}