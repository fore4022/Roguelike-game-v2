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
    private IEnumerator Init()
    {
        yield return new WaitUntil(() => Managers.Game.inGameData.playerData.IsSet);

        Managers.Game.inGameData.playerData.experienceUpdate += ValueUpdate;
        Managers.Game.inGameData.playerData.levelUpdate += MaxValueUpdate;

        Set();
    }
}