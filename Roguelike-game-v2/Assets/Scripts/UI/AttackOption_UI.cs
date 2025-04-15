using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : Button_2
{
    private List<TextMeshProUGUI> textList = new();
    private AttackInformation info = null;
    private Image image;
    private Animator animator;
    private RectTransform imageRect;

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
        imageRect = image.gameObject.GetComponent<RectTransform>();

        SetAnimator(false);
    }
    public void InitOption(AttackInformation info)
    {
        this.info = info;

        UIElementUtility.SetImageScale(rectTransform, minScale);

        SetOption();
    }
    private void SetOption()
    {
        Vector2 size;

        image.sprite = info.data.sprite;
        animator.runtimeAnimatorController = info.data.controller;
        size = image.sprite.bounds.size;

        if(size.x > size.y)
        {
            imageRect.localScale = new Vector3(1, 1 * (size.y / size.x));
        }
        else if(size.y > size.x)
        {
            imageRect.localScale = new Vector3(1 * (size.x / size.y), 1);
        }
        else
        {
            imageRect.localScale = Calculate.GetVector(1);
        }
        
        textList[0].text = $"{info.data.attackType}";
        textList[1].text = $"{info.data.explanation}";

        if(info.caster == null)
        {
            textList[2].text = "New";
        }
        else
        {
            textList[2].text = $"Lv. {info.level + 1}";
        }
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