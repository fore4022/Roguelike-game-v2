using System.Collections;
using UnityEngine;
using TMPro;
public class LevelUp_UI : UserInterface
{
    private TextMeshProUGUI level;
    private Animator animator;

    protected override void Enable()
    {
        StartCoroutine(AnimationPlaying());
    }
    public override void SetUserInterface()
    {
        level = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);

        animator = Util.GetComponentInChildren<Animator>(transform, true);

        gameObject.SetActive(false);
    }
    private IEnumerator AnimationPlaying()
    {
        level.text = $"Lv.{Managers.Game.inGameData.player.Level}";

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        Time.timeScale = 0;

        InputActions.DisableInputAction<TouchControls>();

        Managers.UI.HideUI<CharactorController_UI>();
        Managers.UI.ShowUI<AttackSelection_UI>();
        Managers.UI.HideUI<LevelUp_UI>();
    }
}