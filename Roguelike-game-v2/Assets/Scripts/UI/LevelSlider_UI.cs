using UnityEngine.UI;
public class LevelSlider_UI : UserInterface
{
    private Slider expSlider;

    protected override void Awake()
    {
        base.Awake();

        expSlider = GetComponent<Slider>();

        Managers.UI.HideUI<LevelSlider_UI>();
    }
    private void Start()
    {
        Init();
    }
    private void Set()
    {
        expSlider.maxValue = Managers.Game.inGameData.playerData.ExperienceForLevelUp;
        expSlider.value = Managers.Game.inGameData.playerData.Experience;
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

        Set();
    }
}