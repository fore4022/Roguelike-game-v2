using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public AttackInformation info;

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
        info.levelUpdate.Invoke(++Managers.Game.inGameData.attackData.attackInfo[index].level);
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponentInChildren<Image>();
        animator = GetComponentInChildren<Animator>();
        //GetComponentsInChildren
    }
    private void Start()
    {
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