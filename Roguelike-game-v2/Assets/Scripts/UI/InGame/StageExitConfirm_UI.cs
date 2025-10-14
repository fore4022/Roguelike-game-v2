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

    private const string exitAfterClearMessage = "���������� Ŭ���� �Ǿ����ϴ�.";
    private const string exitWithoutClearWarning = "���������� Ŭ�������� ���߽��ϴ�.\n����ġ�� �׵��� �� �����ϴ�.";

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
        Managers.Data.data.Exp += Managers.Game.UserExp;
        audioSource.clip = giveUpStage_Sound;

        audioSource.Play();
        Managers.UI.Hide<HeadUpDisplay_UI>();
        Managers.Game.GoMain();
    }
    public void OnCancelStageExit()
    {
        audioSource.clip = cancelStageExit_Sound;

        audioSource.Play();
        Managers.UI.Get<PauseMenu_UI>().ShowIcons();
        Managers.UI.Hide<StageExitConfirm_UI>();
    }
}