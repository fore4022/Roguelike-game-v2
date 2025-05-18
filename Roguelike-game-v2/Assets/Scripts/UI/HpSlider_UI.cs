using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HpSlider_UI : UserInterface
{
    private Slider hpSlider;

    public override void SetUserInterface()
    {
        hpSlider = GetComponent<Slider>();

        Util.GetMonoBehaviour().StartCoroutine(WaitPlayerStatInit());
    }
    private void Init()
    {
        Managers.Game.inGameData.player.maxHealthUpdate += MaxValueUpdate;
        Managers.Game.inGameData.player.healthUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    private void MaxValueUpdate()
    {
        hpSlider.maxValue = Managers.Game.inGameData.player.MaxHealth;
    }
    private void ValueUpdate()
    {
        hpSlider.value = Managers.Game.inGameData.player.Health;
    }
    private IEnumerator WaitPlayerStatInit()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        Init();
    }
}