using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : Button_2
{
    public AttackInformation info = null;

    private List<TextMeshProUGUI> textList = new();
    private Image image;
    private Animator animator;

    private Coroutine playAnimation = null;

    public override void OnPointerDown(PointerEventData eventData)
    {
        SetAnimator(true);
        base.OnPointerDown(eventData);
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        SetAnimator(false);
        base.OnPointerExit(eventData);
    }
    protected override void PointerClick()
    {
        StartCoroutine(OnButtonSelected());
    }
    protected override void Init()
    {
        base.Init();

        image = Util.GetComponentInChildren<Image>(transform);
        animator = Util.GetComponentInChildren<Animator>(transform);
        textList = Util.GetComponentsInChildren<TextMeshProUGUI>(transform);

        SetAnimator(false);
    }
    public void InitOption(AttackInformation info)
    {
        this.info = info;
        adjustmentScale = StartCoroutine(Managers.UI.uiElementUtility.SetImageScale(rectTransform, minScale));
        
        SetOption();
    }
    private void SetOption()
    {
        image.sprite = info.data.sprite;
        animator.runtimeAnimatorController = info.data.controller;

        if(info.caster == null)
        {
            textList[0].text = "New";
        }
        else
        {
            textList[0].text = $"Lv.{info.level + 1}";
        }
        
        textList[1].text = $"{info.data.attackType}";
        textList[2].text = $"{info.data.explanation}";
    }
    private void OnDisable()
    {
        animator.runtimeAnimatorController = null;
        info = null;
    }
    private void SetAnimator(bool isEnabled)
    {
        if(animator.enabled == isEnabled)
        {
            return;
        }

        animator.enabled = isEnabled;

        if(isEnabled)
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
        animator.Play(0, 0);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);

        animator.Play(0, 0);

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime == 0);

        SetAnimator(false);
    }
    private IEnumerator OnButtonSelected()
    {
        yield return new WaitUntil(() => animator.enabled == false);

        Managers.Game.inGameData.attack.SetValue(info.data.attackType);
        Managers.UI.GetUI<AttackSelection_UI>().Selected();
    }
}