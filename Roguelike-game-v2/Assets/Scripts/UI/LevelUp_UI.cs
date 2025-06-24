using System.Collections;
using UnityEngine;
using TMPro;
public class LevelUp_UI : UserInterface
{
    private TextMeshProUGUI level;
    private Animator animator;

    protected override void Enable()
    {
        Time.timeScale = 0;
        Managers.Game.IsPlaying = false;

        StartCoroutine(AnimationPlaying());
    }
    public override void SetUserInterface()
    {
        level = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);
        animator = Util.GetComponentInChildren<Animator>(transform, true);

        gameObject.SetActive(false);
    }
    private void Update()
    {
        level.text = $"Lv.{Managers.Game.inGameData.player.Level}";
    }
    private IEnumerator AnimationPlaying()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        InputActions.DisableInputAction<TouchControls>();
        Managers.UI.Hide<CharactorController_UI>();
        Managers.UI.Hide<HeadUpDisplay_UI>();

        if(Managers.Game.inGameData.player.MaxLevel >= Managers.Game.inGameData.player.Level)
        {
            Managers.UI.Show<SkillSelection_UI>();
        }

        Managers.UI.Hide<LevelUp_UI>();
    }
}