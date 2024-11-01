using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler
{
    public AttackInformation info = null;

    private List<TextMeshProUGUI> textList = new();

    private RectTransform rectTransform;
    private Button button;
    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;
    private Coroutine playAnimation = null;

    private const float minScale = 1f;
    private const float maxScale = 1.1f;
    private const float duration = 1;

    private string clipName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAnimator(true);

        if(adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, maxScale, duration));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetAnimator(false);

        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale, duration));
    }
    private void PointerClick()
    {
        Managers.Game.attackData.SetValue(info.data.attackType);

        Managers.UI.ShowUI<Pause_UI>();
        Managers.UI.HideUI<AttackSelection_UI>();
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        image = Util.GetComponentInChildren<Image>(transform);
        animator = Util.GetComponentInChildren<Animator>(transform);

        textList = Util.GetComponentsInChildren<TextMeshProUGUI>(transform);
    } 
    protected override void Start()
    {
        base.Start();

        button.onClick.AddListener(PointerClick);

        SetAnimator(false);
    }
    public void InitOption(int index)
    {
        info = Managers.Game.attackData.attackInfo[index];
        
        SetOption();
    }
    private void SetOption()
    {
        image.sprite = info.data.sprite;

        animator.runtimeAnimatorController = info.data.controller;

        if (info.level == 0)
        {
            textList[0].text = "New";
        }
        else
        {
            textList[0].text = $"Lv.{info.level}";
        }
        
        textList[1].text = $"{info.data.attackType}";
        textList[2].text = $"{info.data.explanation}";
    }
    private void OnDisable()
    {
        animator.runtimeAnimatorController = null;
        info = null;

        InputActions.EnableInputAction<TouchControls>();
    }
    private void SetAnimator(bool isEnabled)
    {
        if(animator.enabled == isEnabled)
        {
            return;
        }

        animator.enabled = isEnabled;

        if (isEnabled)
        {
            playAnimation = StartCoroutine(PlayAnimation());
        }
        else
        {
            if (playAnimation != null)
            {
                StopCoroutine(playAnimation);

                playAnimation = null;
            }

            if (info != null)
            {
                image.sprite = info.data.sprite;
            }
        }
    }
    private IEnumerator PlayAnimation()
    {
        animator.Play(clipName, 0, 0);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        animator.Play(clipName, 0, 0);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 0);

        SetAnimator(false);
    }
}