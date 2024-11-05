using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelUp_UI : UserInterface
{
    private GameObject ui;
    private TextMeshProUGUI level;
    private Animator animator;

    private const float delay = 0.35f;

    private void Awake()
    {
        ui = Util.GetComponentInChildren<Image>(transform, true).gameObject;
        level = Util.GetComponentInChildren<TextMeshProUGUI>(transform, true);
        animator = Util.GetComponentInChildren<Animator>(transform, true);
    }
    protected override void Start()
    {
        base.Start();

        Managers.Game.inGameData.playerData.levelUpdate += AnimationPlay;

        ui.SetActive(false);
    }
    public void AnimationPlay()
    {
        ui.SetActive(true);

        StartCoroutine(AnimationPlaying());
    }
    private IEnumerator AnimationPlaying()
    {
        Managers.UI.HideUI<Pause_UI>();

        level.text = $"Lv.{Managers.Game.inGameData.playerData.Level}";

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        yield return new WaitForSeconds(delay);

        Time.timeScale = 0;

        Managers.UI.ShowUI<AttackSelection_UI>();

        ui.SetActive(false);
    }
}