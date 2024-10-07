using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private AttackInformation_SO info;

    private RectTransform rectTransform;
    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;

    private const float minScale = 1f;
    private const float maxScale = 1.1f;

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
        //
    }
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponentInChildren<Image>();
        animator = GetComponentInChildren<Animator>();

        SetAnimator(false);

        gameObject.SetActive(false);
    }
    public void InitOption(AttackInformation_SO info)
    {
        this.info = info;

        SetOption();

        gameObject.SetActive(true);
    }
    private void SetOption()
    {
        image.sprite = info.sprite;
        animator.runtimeAnimatorController = info.controller;
    }
    private void OnDisable()
    {
        image.sprite = null;
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