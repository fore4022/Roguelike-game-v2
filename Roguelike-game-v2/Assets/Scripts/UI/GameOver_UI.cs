using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameOver_UI : UserInterface
{
    private List<TextMeshProUGUI> tmpList;
    private List<Image> imgList;
    private TextMeshProUGUI result;

    private readonly WaitForSecondsRealtime waitRealSec = new(delay);
    private const float delay = 0.225f;

    public override void SetUserInterface()
    {
        tmpList = Util.GetComponentsInChildren<TextMeshProUGUI>(Util.GetChildren(transform, 1));
        imgList = Util.GetComponentsInChildren<Image>(Util.GetChildren(transform, 1));
        result = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        Managers.UI.HideUI<GameOver_UI>();
    }
    protected override void Enable()
    {
        string result;

        Managers.UI.HideUI<HeadUpDisplay_UI>();

        if(Managers.Game.inGameTimer.GetMinutes > Managers.Game.stageInformation.requiredTime || Managers.Game.inGameTimer.GetHours > 0)
        {
            result = "Stage\nClear";

            Managers.UserData.data.Clear(Managers.UserData.data.StageName);
        }
        else
        {
            result = "Stage\nFailed";
        }

        StartCoroutine(Typing.TypeEffecting(this.result, result, true));
        StartCoroutine(ResultSequence());
    }
    private void OnDisable()
    {
        foreach(TextMeshProUGUI tmp in tmpList)
        {
            tmp.text = "";
        }

        result.text = "";
    }
    public void ReStart()
    {
        StartCoroutine(ActiveHeadUpDisplay());
        Managers.Game.ReStart();
    }
    public void GoMain()
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneName.Main, false);
    }
    private IEnumerator ActiveHeadUpDisplay()
    {
        yield return new WaitForSecondsRealtime(SceneLoading_UI.limitTime);

        Managers.UI.ShowUI<HeadUpDisplay_UI>();
    }
    private IEnumerator ResultSequence()
    {
        Coroutine coroutine = null;
        string required = $"Target Time\n\n{(Managers.Game.stageInformation.requiredTime / 60):D2} : {Managers.Game.stageInformation.requiredTime:D2} : 00";
        string survival = $"Survival Time\n\n{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
        string gainExp;

        if(Managers.Game.UserExp == 0)
        {
            gainExp = "Experience\n\n+ 0 EXP";
        }
        else
        {
            gainExp = $"Experience\n\n+ {Managers.Game.UserExp:N0} EXP";
        }

        yield return waitRealSec;

        yield return Typing.TypeEffectAndWaiting(tmpList[0], required, coroutine, delay);

        yield return Typing.TypeEffectAndWaiting(tmpList[1], required, coroutine, delay);

        yield return Typing.TypeEffectAndWaiting(tmpList[2], survival, coroutine, delay);

        yield return Typing.TypeEffectAndWaiting(tmpList[3], gainExp, coroutine, delay);

        yield return waitRealSec;

        foreach(Image img in imgList)
        {
            UIElementUtility.SetImageAlpha(img, 255, delay, true);
        }
    }
}