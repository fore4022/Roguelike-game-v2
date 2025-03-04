using UnityEngine.UI;
public class ExpSlider_InGame_UI : UserInterface
{
    private Slider expSlider;

    public override void SetUserInterface()
    {
        expSlider = GetComponent<Slider>();

        Init();
    }
    private void MaxValueUpdate()
    {
        expSlider.maxValue = Managers.Game.inGameData.player.ExperienceForLevelUp;
    }
    private void ValueUpdate()
    {
        expSlider.value = Managers.Game.inGameData.player.Experience;
    }
    private void Init()
    {
        Managers.Game.inGameData.player.experienceUpdate += ValueUpdate;
        Managers.Game.inGameData.player.levelUpdate += MaxValueUpdate;

        expSlider.maxValue = Managers.Game.inGameData.player.ExperienceForLevelUp;
        expSlider.value = Managers.Game.inGameData.player.Experience;
    }
}