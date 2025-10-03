using UnityEngine.UI;
public class ExpSlider_UI : UserInterface
{
    private Slider expSlider;

    public override void SetUserInterface()
    {
        expSlider = GetComponent<Slider>();

        Init();
    }
    private void Init()
    {
        Managers.Game.inGameData_Manage.player.levelUpdate += MaxValueUpdate;
        Managers.Game.inGameData_Manage.player.experienceUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    private void MaxValueUpdate()
    {
        expSlider.maxValue = Managers.Game.inGameData_Manage.player.ExperienceForLevelUp;
    }
    private void ValueUpdate()
    {
        expSlider.value = Managers.Game.inGameData_Manage.player.Experience;
    }
}