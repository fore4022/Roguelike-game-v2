using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class HpSlider_UI : UserInterface
{
    private Slider hpSlider;

    public override void SetUserInterface()
    {
        hpSlider = GetComponent<Slider>();

        Coroutine_Helper.StartCoroutine(WaitPlayerStatInit());
    }
    private void Init()
    {
        Managers.Game.player.maxHealthUpdate += MaxValueUpdate;
        Managers.Game.player.healthUpdate += ValueUpdate;

        MaxValueUpdate();
        ValueUpdate();
    }
    private void MaxValueUpdate()
    {
        hpSlider.maxValue = Managers.Game.player.MaxHealth;
    }
    private void ValueUpdate()
    {
        hpSlider.value = Managers.Game.player.Health;
    }
    private IEnumerator WaitPlayerStatInit()
    {
        yield return new WaitUntil(() => Managers.Game.player != null);

        Init();
    }
}