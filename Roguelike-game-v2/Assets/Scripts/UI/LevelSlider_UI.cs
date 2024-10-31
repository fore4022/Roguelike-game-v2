using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LevelSlider_UI : UserInterface
{
    private Slider expSlider;

    private void Awake()
    {
        expSlider = GetComponent<Slider>();
    }
    protected override void Start()
    {
        base.Start();

        StartCoroutine(Init());
    }
    private void Set()
    {
        expSlider.maxValue = Managers.Game.playerData.ExperienceForLevelUp;
        expSlider.value = Managers.Game.playerData.Experience;
    }
    private void MaxValueUpdate()
    {
        expSlider.maxValue = Managers.Game.playerData.ExperienceForLevelUp;
    }
    private void ValueUpdate()
    {
        expSlider.value = Managers.Game.playerData.Experience;
    }
    private IEnumerator Init()
    {
        yield return new WaitUntil(() => Managers.Game.playerData.IsSet);

        Managers.Game.playerData.experienceUpdate += ValueUpdate;
        Managers.Game.playerData.levelUpdate += MaxValueUpdate;

        Set();
    }
}