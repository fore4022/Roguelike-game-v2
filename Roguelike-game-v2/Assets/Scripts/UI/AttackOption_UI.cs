using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : UserInterface, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AttackInformation info;

    private List<TextMeshProUGUI> textList = new();

    private RectTransform rectTransform;
    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;

    private const float minScale = 1f;
    private const float maxScale = 1.1f;

    private int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetAnimator(true);

        if(adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        adjustmentScale = StartCoroutine(SetImageScale(maxScale));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        SetAnimator(false);

        if (adjustmentScale != null)
        {
            StopCoroutine(adjustmentScale);
        }

        StartCoroutine(SetImageScale(minScale));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        info.caster = Managers.Game.attackCasterManage.CreateAndGetCaster(info.data.attackType);
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = Util.GetComponentInChildren<Image>(transform);
        animator = Util.GetComponentInChildren<Animator>(transform);

        textList = Util.GetComponentsInChildren<TextMeshProUGUI>(transform);
    }
    protected override void Start()
    {
        base.Start();

        SetAnimator(false);
    }
    public void InitOption(int index)
    {
        this.index = index;

        info = Managers.Game.inGameData.attackData.attackInfo[index];
        
        SetOption();
    }
    private void SetOption()
    {
        image.sprite = info.data.sprite;
        animator.runtimeAnimatorController = info.data.controller;

        if(info.level == 0)
        {
            textList[0].text = $"Lv.{info.level}";
        }
        else
        {
            textList[0].text = $"New";
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
        animator.enabled = isEnabled;
    }
    private IEnumerator SetImageScale(float targetScale)
    {
        Vector3 scale;

        float totalTime = 0;
        float scaleValue;

        while (rectTransform.localScale.x != targetScale)
        {
            totalTime += Time.deltaTime;

            if(totalTime > 1)
            {
                totalTime = 1;
            }

            scaleValue = Mathf.Lerp(rectTransform.localScale.x, targetScale, totalTime);

            scale = new Vector3(scaleValue, scaleValue);
            
            rectTransform.localScale = scale;

            yield return null;
        }
    }
}