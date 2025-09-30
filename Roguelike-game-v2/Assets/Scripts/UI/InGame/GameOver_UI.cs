using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class GameOver_UI : UserInterface
{
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip clear;
    [SerializeField]
    private AudioClip failed;
    [SerializeField]
    private AudioClip buttonClickSfx;

    private List<TextMeshProUGUI> tmpList;
    private List<Image> imgList;
    private TextMeshProUGUI result;

    private const string arrow = "->";
    private const float delay = 0.225f;
    private readonly WaitForSecondsRealtime waitRealSec = new(delay);

    public override void SetUserInterface()
    {
        audioSource = GetComponent<AudioSource>();
        tmpList = Util.GetComponentsInChildren<TextMeshProUGUI>(Util.GetChildren(transform, 1), true);
        imgList = Util.GetComponentsInChildren<Image>(Util.GetChildren(transform, 1));
        result = Util.GetComponentInChildren<TextMeshProUGUI>(transform);

        Managers.UI.Hide<GameOver_UI>();
    }
    protected override void Enable()
    {
        string result;

        Time.timeScale = 0;

        Managers.UI.Hide<HeadUpDisplay_UI>();

        if(Managers.Game.IsStageClear || Managers.Game.inGameTimer.GetHours > 0)
        {
            audioSource.clip = clear;
            result = "Stage\nClear";

            Managers.UserData.data.Clear(Managers.UserData.data.StageName);
        }
        else
        {
            audioSource.clip = failed;
            result = "Stage\nFailed";
        }

        audioSource.Play();
        StartCoroutine(Typing.TypeEffecting(this.result, result, true));
        StartCoroutine(ResultSequence());
    }
    private void OnDisable()
    {
        foreach(Image img in imgList)
        {
            UIElementUtility.SetImageAlpha(img, 0);
            img.gameObject.SetActive(false);
        }

        foreach(TextMeshProUGUI tmp in tmpList)
        {
            tmp.text = "";
        }

        tmpList[0].gameObject.SetActive(true);
        tmpList[1].gameObject.SetActive(false);
        tmpList[2].gameObject.SetActive(false);

        tmpList[0].rectTransform.anchoredPosition = new(-175, 195);
        tmpList[1].rectTransform.anchoredPosition = new(175, 195);
        tmpList[2].rectTransform.anchoredPosition = new(175, 195);

        result.text = "";
    }
    public void Play()
    {
        Time.timeScale = 1;
        Managers.Game.Playing = true;

        audioSource.Play();
        Managers.UI.Show<HeadUpDisplay_UI>();
        Managers.UI.Hide<GameOver_UI>();
    }
    public void ReStart()
    {
        audioSource.Play();
        Managers.Game.ReStart();
    }
    public void GoMain()
    {
        audioSource.Play();
        Managers.Game.GoMain();
    }
    private IEnumerator ResultSequence()
    {
        Coroutine coroutine = null;
        string required = $"목표 시간\n\n{(Managers.Game.stageInformation.requiredTime / 60):D2} : {Managers.Game.stageInformation.requiredTime:D2} : 00";
        string survival = $"생존 시간\n\n{Managers.Game.inGameTimer.GetHours:D2} : {Managers.Game.inGameTimer.GetMinutes:D2} : {Managers.Game.inGameTimer.GetSeconds:D2}";
        string gainExp = $"Experience\n\n+ {Managers.Game.UserExp:N0} EXP";

        yield return waitRealSec;

        yield return Typing.TypeEffectAndWaiting(tmpList[0], required, coroutine, delay);

        tmpList[0].SetPosition(new(-175, 195), delay);
        tmpList[1].gameObject.SetActive(true);

        yield return Typing.TypeEffectAndWaiting(tmpList[1], arrow, coroutine);

        tmpList[1].SetPosition(new(-175, 195), delay);

        StartCoroutine(Typing.EraseEffecting(tmpList[0], delay));

        yield return new WaitForSecondsRealtime(delay);

        tmpList[0].gameObject.SetActive(false);
        tmpList[2].gameObject.SetActive(true);

        yield return Typing.TypeEffectAndWaiting(tmpList[2], survival, coroutine);

        tmpList[2].SetPosition(new(0, 195), delay);

        StartCoroutine(Typing.EraseEffecting(tmpList[1], delay));

        yield return new WaitForSecondsRealtime(delay);

        tmpList[1].gameObject.SetActive(false);

        yield return Typing.TypeEffectAndWaiting(tmpList[3], gainExp, coroutine, delay);

        yield return waitRealSec;

        audioSource.clip = buttonClickSfx;

        if(Managers.Game.IsStageClear && !Managers.Game.GameOver)
        {
            imgList[0].gameObject.SetActive(true);
            imgList[1].gameObject.SetActive(false);
        }
        else
        {
            imgList[0].gameObject.SetActive(false);
            imgList[1].gameObject.SetActive(true);
        }

        imgList[2].gameObject.SetActive(true);

        foreach(Image img in imgList)
        {
            UIElementUtility.SetImageAlpha(img, 255, delay, true);
        }

        Time.timeScale = 0;
    }
}