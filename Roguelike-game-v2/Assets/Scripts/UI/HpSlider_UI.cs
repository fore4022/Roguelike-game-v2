using UnityEngine.UI;
public class HpSlider_UI : UserInterface
{
    private Slider hpSlider;

    public override void SetUserInterface()
    {
        hpSlider = GetComponent<Slider>();

        Init();
    }
    private void Init()
    {
        Managers.Game.inGameData.player.maxHealthUpdate += MaxValueUpdate;
        Managers.Game.inGameData.player.healthUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    public void MaxValueUpdate()
    {
        hpSlider.maxValue = Managers.Game.inGameData.player.MaxHealth;
    }
    private void ValueUpdate()
    {
        hpSlider.value = Managers.Game.inGameData.player.Health;
    }
}