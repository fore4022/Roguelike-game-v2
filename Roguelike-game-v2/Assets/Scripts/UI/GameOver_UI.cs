using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class GameOver_UI : UserInterface
{
    private List<TextMeshProUGUI> tmpList;
    private List<Image> imgList;
    private TextMeshProUGUI result;

    private const string arrow = "->";
    private const float delay = 0.225f;
    private readonly WaitForSecondsRealtime waitRealSec = new(delay);

    public override void SetUserInterface()
    {
        tmpList = Util.GetComponentsInChildren<TextMeshProUGUI>(Util.GetChildren(transform, 1), true);
        imgList = Util.GetComponentsInChildren<Image>(Util.GetChildren(transform, 1));
        result = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        Managers.UI.Hide<GameOver_UI>();
    }
    protected override void Enable()
    {
        string result;

        Managers.UI.Hide<HeadUpDisplay_UI>();

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

        tmpList[0].gameObject.SetActive(true);
        tmpList[1].gameObject.SetActive(false);
        tmpList[2].gameObject.SetActive(false);

        result.text = "";
    }
    public void ReStart()
    {
        Managers.Game.ReStart();
    }
    public void GoMain()
    {
        Managers.Game.Clear();
        Managers.Scene.LoadScene(SceneName.Main, false);
    }
    private IEnumerator ResultSequence()
    {
        Coroutine coroutine = null;
        string required = $"Target Time\n\n{(Managers.Game.stageInformation.requiredTime / 60):D2} : {Managers.Game.stageInformation.requiredTime:D2} : 00";
        string survival = $"Survival Time\n\n{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
        string gainExp = $"Experience\n\n+ {Managers.Game.UserExp:N0} EXP";

        Managers.UserData.data.Exp += Managers.Game.UserExp;
        Managers.Game.UserExp = 0;

        yield return waitRealSec;

        yield return Typing.TypeEffectAndWaiting(tmpList[0], required, coroutine, delay);

        tmpList[0].SetPosition(new(-175, 260), delay);
        tmpList[1].gameObject.SetActive(true);

        yield return Typing.TypeEffectAndWaiting(tmpList[1], arrow, coroutine);

        tmpList[1].SetPosition(new(-175, 260), delay);

        StartCoroutine(Typing.EraseEffecting(tmpList[0], delay));

        yield return new WaitForSecondsRealtime(delay);

        tmpList[0].gameObject.SetActive(false);
        tmpList[2].gameObject.SetActive(true);

        yield return Typing.TypeEffectAndWaiting(tmpList[2], survival, coroutine);

        tmpList[2].SetPosition(new(0, 260), delay);

        StartCoroutine(Typing.EraseEffecting(tmpList[1], delay));

        yield return new WaitForSecondsRealtime(delay);

        tmpList[1].gameObject.SetActive(false);

        yield return Typing.TypeEffectAndWaiting(tmpList[3], gainExp, coroutine, delay);

        yield return waitRealSec;

        foreach(Image img in imgList)
        {
            UIElementUtility.SetImageAlpha(img, 255, delay, true);
        }
    }
}