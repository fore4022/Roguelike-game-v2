using UnityEngine;
public class StageSelection_UI : UserInterface
{
    [SerializeField]
    private Button_2[] nextStageButtons;

    public override void SetUserInterface()
    {
        foreach(Button_2 ui in nextStageButtons)
        {
            ui.Set();
        }
    }
}