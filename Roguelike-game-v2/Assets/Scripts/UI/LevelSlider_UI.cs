using UnityEngine.UI;
public class LevelSlider_UI : UserInterface
{
    private Slider expSlider;

    public override void SetUserInterface()
    {
        expSlider = GetComponent<Slider>();

        Init();
    }
    private void MaxValueUpdate()
    {
        expSlider.maxValue = Managers.Game.inGameData.playerData.ExperienceForLevelUp;
    }
    private void ValueUpdate()
    {
        expSlider.value = Managers.Game.inGameData.playerData.Experience;
    }
    private void Init()
    {
        Managers.Game.inGameData.playerData.experienceUpdate += ValueUpdate;
        Managers.Game.inGameData.playerData.levelUpdate += MaxValueUpdate;

        expSlider.maxValue = Managers.Game.inGameData.playerData.ExperienceForLevelUp;
        expSlider.value = Managers.Game.inGameData.playerData.Experience;
    }
}