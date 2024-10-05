using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AttackInformation_SO info;

    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;

    private const float scaleUpdateSpeed = 2;
    private const float minScale = 200;
    private const float maxScale = 325;

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
    private void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

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
        //image.sprite = null;
        //animator.runtimeAnimatorController = null;
        info = null;
    }
    private void SetAnimator(bool isEnabled)
    {
        animator.enabled = isEnabled;
    }
    private IEnumerator SetImageScale(float targetScale)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float scale = (targetScale - rectTransform.rect.width) * scaleUpdateSpeed;

        while ((int)rectTransform.rect.width != targetScale)
        {
            rectTransform.sizeDelta += new Vector2(scale, scale)* Time.deltaTime;

            Debug.Log(rectTransform.rect);

            yield return null;
        }

        Debug.Log("asdf");
    }
}