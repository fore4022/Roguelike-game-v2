using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class AttackOption_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private AttackInformation_SO info;

    private RectTransform rectTransform;
    private Image image;
    private Animator animator;

    private Coroutine adjustmentScale = null;

    //private const float scaleUpdateSpeed = 2;
    private const float minScale = 1f;
    private const float maxScale = 1.625f;

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
        //SetAnimator(false);

        //if (adjustmentScale != null)
        //{
        //    StopCoroutine(adjustmentScale);
        //}

        //StartCoroutine(SetImageScale(minScale));
    } 
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();

        SetAnimator(false);

        //gameObject.SetActive(false);
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
        Vector3 scale;

        float totalTime = 0;

        Debug.Log("start");

        while (rectTransform.localScale.x != targetScale)
        {
            Debug.Log(totalTime);

            totalTime += Time.deltaTime;

            if(totalTime > 1)
            {
                totalTime = 1;
            }

            scale = new Vector3(targetScale, targetScale) * totalTime;

            Debug.Log(totalTime);

            Debug.Log(scale);

            rectTransform.localScale = scale;

            Debug.Log(rectTransform.localScale);

            Debug.Log(rectTransform.localScale.x);

            Debug.Log("-");

            yield return null;
        }
    }
}