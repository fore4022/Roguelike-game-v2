using TMPro;
using UnityEngine;
public class StageExitConfirm_UI : UserInterface
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip giveUpStage_Sound;
    [SerializeField]
    private AudioClip cancelStageExit_Sound;
    [SerializeField]
    private TextMeshProUGUI tmp;

    private const string exitAfterClearMessage = "The stage has been cleared.";
    private const string exitWithoutClearWarning = "The stage was not cleared.";

    public override void SetUserInterface()
    {
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
    protected override void Enable()
    {
        if(Managers.Game.IsStageClear)
        {
            tmp.text = exitAfterClearMessage;
        }
        else
        {
            tmp.text = exitWithoutClearWarning;
        }

        Managers.UI.Get<PauseMenu_UI>().HideIcons();
    }
    public void OnGiveUpStage()
    {
        audioSource.clip = giveUpStage_Sound;

        audioSource.Play();
        Managers.UI.Hide<HeadUpDisplay_UI>();
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneName.Main, false);
    }
    public void OnCancelStageExit()
    {
        audioSource.clip = cancelStageExit_Sound;

        audioSource.Play();
        Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
}