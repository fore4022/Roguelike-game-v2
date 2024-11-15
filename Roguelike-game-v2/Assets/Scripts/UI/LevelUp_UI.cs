using System.Collections;
using UnityEngine;
using TMPro;
public class LevelUp_UI : UserInterface
{
    private TextMeshProUGUI level;
    private Animator animator;

    private const float delay = 0.35f;

    protected override void OnEnable()
    {
        base.OnEnable();

        StartCoroutine(AnimationPlaying());
    }
    public override void SetUI()
    {
        level = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);
        animator = Util.GetComponentInChildren<Animator>(transform, true);

        gameObject.SetActive(false);
    }
    private IEnumerator AnimationPlaying()
    {
        level.text = $"Lv.{Managers.Game.inGameData.playerData.Level}";

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        yield return new WaitForSeconds(delay);

        Time.timeScale = 0;

        Managers.UI.ShowUI<AttackSelection_UI>();
        Managers.UI.HideUI<LevelUp_UI>();
    }
}