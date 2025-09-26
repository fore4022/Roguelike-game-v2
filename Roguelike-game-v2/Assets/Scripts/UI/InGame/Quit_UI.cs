public class Quit_UI : Button_A
{
    protected override void PointerClick()
    {
        Managers.UI.Show<StageExitConfirm_UI>();
    }
}